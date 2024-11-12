import pandas as pd
import matplotlib.pyplot as plt
import prince
from sklearn_extra.cluster import KMedoids
from sklearn.metrics import silhouette_score
import scipy.stats as stats


#-----------------------------------------DATA PREPARATION --------------------------------------------
#data acquisition
data = pd.read_csv('C:/Users/simom/Desktop/e-health/project/docs/dataset_project_eHealth20242025.csv') #the path is taken from my computer, change it to try on yours

#data preliminary overview
print(data.head()) #to know the type of information available
print(data.info()) #to observe the data types of the columns
print(data.describe()) #to have a basic statistic overview

#definition of columns of interest
demographic_cols = ['age', 'gender', 'education', 'marital', 'income']
psychometric_cols = [col for col in data.columns if 'phq' in col or 'gad' in col or 'asrs' in col or 'asq' in col]

relevant_data = data[demographic_cols + psychometric_cols].copy()

#filling missing values
missing_columns = relevant_data.columns[relevant_data.isnull().any()]

#we impute categorical missing data with the mode and numerical missing data with the mean
for col in missing_columns:
    if relevant_data[col].dtype == 'float64' or relevant_data[col].dtype == 'int64':
        relevant_data[col].fillna(relevant_data[col].mean(), inplace=True)
    else:
        relevant_data[col].fillna(relevant_data[col].mode()[0], inplace=True)

#descriptive analysis
#histogram of all the demografic variables
for col in demographic_cols:
    relevant_data[col].hist(bins=20)
    plt.title(f'"{col}" distribution')
    plt.xlabel(col)
    plt.ylabel('Frequency')
    plt.show()

"""
#this section is commented because we discovered that there is no interesting correlation between the used variables
#scatter plot between age and GAD-7 scores (anxiety)
plt.figure(figsize=(8, 5))
plt.scatter(relevant_data['age'], relevant_data['gad_7'], alpha=0.5, color='blue')
plt.title('Relationship between age and anxiety')
plt.xlabel('Age')
plt.ylabel('GAD-7 score')
plt.show()

#scatter plot between income and GAD-7 scores (anxiety)
plt.figure(figsize=(8, 5))
plt.scatter(relevant_data['income'], relevant_data['gad_7'], alpha=0.5, color='orange')
plt.title('Relationship between income and anxiety')
plt.xlabel('Income')
plt.ylabel('GAD-7 score')
plt.show()

#scatter plot between marital status and PHQ-9 scores (depression)
plt.figure(figsize=(8, 5))
plt.scatter(relevant_data['marital'], relevant_data['phq_9'], alpha=0.5, color='blue')
plt.title('Relationship between marital status and depression')
plt.xlabel('Marital status')
plt.ylabel('PHQ-9 score')
plt.show()
"""

#FAMD
famd = prince.FAMD(n_components=50, random_state=42)
relevant_data_transformed = famd.fit_transform(relevant_data)
famd.eigenvalues_summary

#-------------------------------CLUSTERING---------------------------------------------------
#%%
#trying different clusters to choose the best one
silhouette_scores = []
elbow_inertia = []
K = range(2, 10)  #Our range to explore

for k in K:
    kmedoids = KMedoids(n_clusters=k, random_state=42)
    cluster_labels = kmedoids.fit_predict(relevant_data_transformed)

    #Inertia calculation
    elbow_inertia.append(kmedoids.inertia_)

#Elbow method graphic
plt.figure(figsize=(10, 5))
plt.plot(K, elbow_inertia, marker='o')
plt.xlabel('Number of Cluster')
plt.ylabel('Inertia')
plt.title('Elbow method to determine the number of clusters')
plt.grid(True)
plt.show()

#Final clustering with 7 clusters
optimal_k = 7
kmedoids_final = KMedoids(n_clusters=optimal_k, random_state=42)
cluster_labels_final = kmedoids_final.fit_predict(relevant_data_transformed)
relevant_data_transformed['Cluster'] = cluster_labels_final


#%%
#-------------------------------STATISTICAL ANALYSIS--------------------------------------------------------------
cluster_summary = relevant_data_transformed.groupby('Cluster').mean()
print(cluster_summary)

#adding cluster labels to the original dataset
data['Cluster'] = cluster_labels_final

#calculating statistics
cluster_summary_original = data.groupby('Cluster').mean()
print(cluster_summary_original)


#dividing categorical vars from quantitative ones
categorical_vars = ['marital', 'education', 'gender']  # Adjust these to actual categorical columns
quantitative_vars = []
for col in data.columns:
    if col not in categorical_vars:
        quantitative_vars.append(col)

#analyzing categorical variables per cluster
def analyze_categorical(variable, data):
    #frequency table by cluster for each category
    cluster_counts = data.groupby('Cluster')[variable].value_counts(normalize=True).unstack()
    #chi-square test to identify significant differences across clusters
    chi2, p_value, _, _ = stats.chi2_contingency(data.pivot_table(index='Cluster', columns=variable, aggfunc='size', fill_value=0))
    return cluster_counts, p_value

#analyzing quantitative variables per cluster
def analyze_quantitative(variable, data):
    #group data by cluster and obtain the lists for each cluster group
    cluster_data = [data[data['Cluster'] == k][variable].dropna() for k in data['Cluster'].unique()]
    #Kruskal-Wallis test to identify significant differences
    kruskal_result = stats.kruskal(*cluster_data)
    #Median and IQR for each cluster
    medians = data.groupby('Cluster')[variable].median()
    iqr = data.groupby('Cluster')[variable].quantile([0.25, 0.75]).unstack()
    return medians, iqr, kruskal_result.pvalue

#storage of results for each categorical variable
categorical_results = {}
for var in categorical_vars:
    counts, p_val = analyze_categorical(var, data)  # Perform analysis
    categorical_results[var] = (counts, p_val)  # Store results

#storage of results for each quantitative variable
quantitative_results = {}
for var in quantitative_vars:
    medians, iqr, p_val = analyze_quantitative(var, data)  # Perform analysis
    quantitative_results[var] = (medians, iqr, p_val)  # Store results

#creation of a text file for the storage of results
output_file = 'C:/Users/simom/Desktop/e-health/project/results.txt' #the path is taken from my computer, change it to try on yours

with open(output_file, 'w') as f:
    f.write("Categorical variables results:\n")
    for var, (counts, p_val) in categorical_results.items():
        f.write(f"\n{var}:\nCounts per cluster:\n{counts}\nP-value: {p_val}\n")

    f.write("\n\nQuantitative variables results:\n")
    for var, (medians, iqr, p_val) in quantitative_results.items():
        f.write(f"\n{var}:\nMedian per cluster:\n{medians}\nIQR per cluster:\n{iqr}\nP-value: {p_val}\n")

print(f"Results saved in: {output_file}")