using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalZone : MonoBehaviour
{
    public string sampleSceneName = "SampleScene";      // Name of the first game scene
    public string forestSceneName = "ForestOfTension"; // Name of the second game scene
    public string endingSceneName = "GameOver";        // Name of the ending scene
    public int nextStoryIndex; // Index for the storyline to show after the goal event

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRopeController player = other.GetComponent<PlayerRopeController>();
            if (player != null)
            {
                player.messageText.text = "Congratulations! You won!";

                PlayerPrefs.SetInt("ForestSceneCompleted", 1);


                // Check which game is next
                bool sampleCompleted = PlayerPrefs.GetInt("SampleSceneCompleted", 0) == 1;

                // Print the values to the console
                Debug.Log("Sample Scene Completed: " + sampleCompleted);


                string nextScene;
                if (!sampleCompleted)
                {
                    nextScene = sampleSceneName;
                    PlayerPrefs.SetInt("SampleSceneCompleted", 1);
                    nextStoryIndex = 5; // Start at 16 after the storyline ends here
                }
                else
                {
                    nextScene = endingSceneName; // End the game
                    nextStoryIndex = 21;        // Final storyline segment
                }

                Debug.Log($"GoalZone triggered! NextScene: {nextScene}, StoryIndex: {nextStoryIndex}");

                // Save the next scene and storyline progress
                PlayerPrefs.SetString("NextScene", nextScene);
                PlayerPrefs.SetInt("StoryIndex", nextStoryIndex);
                PlayerPrefs.Save();

                // Load the StorylineScene
                SceneManager.LoadScene("StorylineScene");
            }
        }
    }
}
