using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
    }

    public TMP_Text questionText;
    public Button[] answerButtons;

    private List<Question> questions;
    private int currentQuestionIndex = 0;

    // Scoring for player profiles
    private int alessandraScore = 0; // High stress, burnout, somatization
    private int carloScore = 0;      // Low stress, high organization
    private int lukeScore = 0;       // Moderate stress, resilient
    private int mariaScore = 0;      // Balanced, but stressed with responsibilities

    void Start()
    {
        InitializeQuestions();
        DisplayNextQuestion();
    }

    void InitializeQuestions()
    {
        questions = new List<Question>
        {
            new Question
            {
                questionText = "How do you generally feel about your work or daily responsibilities?",
                answers = new string[]
                {
                    "A. I often feel overwhelmed and stressed.",
                    "B. I feel generally calm and well-organized.",
                    "C. It is challenging, but I manage fairly well.",
                    "D. I often feel tired, but I keep pushing forward."
                }
            },
            new Question
            {
                questionText = "What is your approach when facing a stressful problem?",
                answers = new string[]
                {
                    "A. I worry a lot and tend to somatize my feelings.",
                    "B. I stay calm and focus on solving it step by step.",
                    "C. I try to find solutions while managing the stress.",
                    "D. I work through it, though I sometimes feel anxious or exhausted."
                }
            },
            new Question
            {
                questionText = "How would you describe your energy level throughout the day?",
                answers = new string[]
                {
                    "A. Often low, I feel fatigued or burnt out.",
                    "B. Stable, I feel efficient and motivated.",
                    "C. Variable, it depends on the day and challenges.",
                    "D. Sometimes tired, but I try to stay active."
                }
            },
            new Question
            {
                questionText = "Do you experience physical symptoms (like headaches or muscle tension) related to stress?",
                answers = new string[]
                {
                    "A. Often.",
                    "B. Rarely or never.",
                    "C. Occasionally, but I manage them.",
                    "D. Sometimes, especially during busy periods."
                }
            },
            new Question
            {
                questionText = "How would you rate your ability to maintain work-life balance?",
                answers = new string[]
                {
                    "A. Difficult, work often invades my personal life.",
                    "B. Very good, I balance the two effectively.",
                    "C. Moderate, I�m working on improving it.",
                    "D. Variable, it depends on circumstances."
                }
            },
            new Question
            {
                questionText = "Which of the following statements best reflects your personality?",
                answers = new string[]
                {
                    "A. I deeply dedicate myself to others, but this sometimes causes me stress.",
                    "B. I am organized and rarely let stress affect me.",
                    "C. I am resilient, but sometimes feel overwhelmed by challenges.",
                    "D. I am responsible, but the weight of my responsibilities sometimes stresses me."
                }
            }
        };
    }

    void DisplayNextQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            ShowResult();
            return;
        }

        Question currentQuestion = questions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentQuestion.answers[i];

                int scoreToAdd = i; // Lower index = stronger alignment with "A" answers
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => HandleAnswer(scoreToAdd));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void HandleAnswer(int answerScore)
    {
        // Assign scores based on current question
        switch (currentQuestionIndex)
        {
            case 0: // Question 1
            case 3: // Question 4
                alessandraScore += (3 - answerScore);
                carloScore += answerScore;
                break;

            case 1: // Question 2
            case 4: // Question 5
                lukeScore += (2 - answerScore);
                mariaScore += (answerScore == 3 ? 1 : 0);
                break;

            case 2: // Question 3
                alessandraScore += (3 - answerScore);
                lukeScore += answerScore;
                break;

            case 5: // Question 6
                alessandraScore += (3 - answerScore);
                mariaScore += (answerScore == 3 ? 1 : 0);
                break;
        }

        currentQuestionIndex++;
        DisplayNextQuestion();
    }

    void ShowResult()
    {
        string nextScene = "SampleScene"; // Default scene

        if (alessandraScore >= carloScore && alessandraScore >= lukeScore && alessandraScore >= mariaScore)
        {
            Debug.Log("Player assigned to Positive Thinking Minigame");
            nextScene = "SampleScene";
        }
        else if (carloScore >= alessandraScore && carloScore >= lukeScore && carloScore >= mariaScore)
        {
            Debug.Log("Player assigned to Focus Minigame");
            nextScene = "Swamp of Digestive Woes";
        }
        else if (lukeScore >= alessandraScore && lukeScore >= carloScore && lukeScore >= mariaScore)
        {
            Debug.Log("Player assigned to Breathing Minigame");
            nextScene = "ForestOfTension";
        }

        // Save and load the result scene
        PlayerPrefs.SetString("NextScene", nextScene);
        PlayerPrefs.Save();
        SceneManager.LoadScene(nextScene);
    }


}
