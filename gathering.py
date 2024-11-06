import tkinter as tk

questions = [
    "How would you rate the overall experience?",
    "How satisfied are you with the support?",
    "How easy was it to use the tool?",
    # Add more questions as needed
]
ratings = {q: 0 for q in questions}  # Initialize ratings to zero or a default value

def submit_ratings():
    for question, scale in rating_scales.items():
        ratings[question] = scale.get()
    print(ratings)  # Or save results as needed

app = tk.Tk()
app.title("Questionnaire")

rating_scales = {}
for i, question in enumerate(questions):
    question_label = tk.Label(app, text=question)
    question_label.grid(row=i, column=0, padx=10, pady=5)
    
    scale = tk.Scale(app, from_=1, to=5, orient="horizontal")
    scale.grid(row=i, column=1, padx=10, pady=5)
    rating_scales[question] = scale

submit_button = tk.Button(app, text="Submit", command=submit_ratings)
submit_button.grid(row=len(questions), column=0, columnspan=2, pady=10)

app.mainloop()


