using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public string[] storyline;
    public float typingSpeed = 0.05f;
    private int currentIndex = 0;

    void Start()
    {
        // Load saved progress or use defaults
        currentIndex = PlayerPrefs.GetInt("StoryIndex", 0);
        string nextScene = PlayerPrefs.GetString("NextScene", "SampleScene");

        Debug.Log($"Starting storyline from index: {currentIndex}, NextScene: {nextScene}");
        StartCoroutine(DisplayStoryline());
    }

    IEnumerator DisplayStoryline()
    {
        while (currentIndex < storyline.Length)
        {
            yield return StartCoroutine(TypeText(storyline[currentIndex]));
            currentIndex++;

                    if (currentIndex == 4)
        {
            Debug.Log("Finished the first 4 storyline messages. Loading QuizScene.");
            PlayerPrefs.SetInt("StoryIndex", currentIndex); // Save progress
            PlayerPrefs.Save();
            SceneManager.LoadScene("QuizScene");
            yield break; // Stop the coroutine for now
        }


            // After 11 messages, transition to the next scene
            if (currentIndex == 10)
            {
                string nextScene = PlayerPrefs.GetString("NextScene", "SampleScene");
                Debug.Log($"Finished 11 storyline messages. Loading the next scene: {nextScene}.");
                PlayerPrefs.SetInt("StoryIndex", currentIndex);
                PlayerPrefs.Save();
                SceneManager.LoadScene(nextScene);
                yield break;
            }

            // After 16 messages, transition to the next game or ending
            if (currentIndex == 17)
            {
                string nextScene = PlayerPrefs.GetString("NextScene", "ForestOfTension");
                Debug.Log($"Finished 16 storyline messages. Loading the next scene: {nextScene}.");
                PlayerPrefs.SetInt("StoryIndex", currentIndex);
                PlayerPrefs.Save();
                SceneManager.LoadScene(nextScene);
                yield break;
            }

            // After 20 messages, end the game
            if (currentIndex == 21)
            {
                string endingScene = PlayerPrefs.GetString("NextScene", "GameOver");
                Debug.Log($"Finished final storyline messages. Ending the game: {endingScene}.");
                PlayerPrefs.SetInt("StoryIndex", currentIndex);
                PlayerPrefs.Save();
                SceneManager.LoadScene(endingScene);
                yield break;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator TypeText(string text)
    {
        textMeshPro.text = "";
        foreach (char c in text)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}



