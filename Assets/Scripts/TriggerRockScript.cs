using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerRock : MonoBehaviour
{
    public string sampleSceneName = "SampleScene";      // Name of the first game scene
    public string forestSceneName = "ForestOfTension"; // Name of the second game scene
    public string endingSceneName = "GameOver";        // Name of the ending scene
    public int nextStoryIndex; // Index for the storyline to show after the rock event

    void OnDestroy()
    {
        // Check which game is next
        PlayerPrefs.SetInt("SampleSceneCompleted", 1);
        bool forestCompleted = PlayerPrefs.GetInt("ForestSceneCompleted", 0) == 1;

        string nextScene;



                Debug.Log("Forest Scene Completed: " + forestCompleted);


        if (!forestCompleted)
        {
            nextScene = forestSceneName;
            PlayerPrefs.SetInt("ForestSceneCompleted", 1);
            nextStoryIndex = 17; // Start at 16 after the storyline ends here
        }
        else
        {
            nextScene = endingSceneName; // End the game
            nextStoryIndex = 21;        // Final storyline segment
        }

        Debug.Log($"Trigger rock destroyed! NextScene: {nextScene}, StoryIndex: {nextStoryIndex}");

        // Save the next scene and storyline progress
        PlayerPrefs.SetString("NextScene", nextScene);
        PlayerPrefs.SetInt("StoryIndex", nextStoryIndex);
        PlayerPrefs.Save();

        // Load the StorylineScene
        SceneManager.LoadScene("StorylineScene");
    }
}
