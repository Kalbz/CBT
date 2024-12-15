using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("PlayGame method called");
        SceneManager.LoadScene("StorylineScene");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame method called");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
