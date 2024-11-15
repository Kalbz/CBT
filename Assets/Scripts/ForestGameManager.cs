using UnityEngine;
using TMPro;

public class ForestGameManager : MonoBehaviour
{
    public PlayerRopeController player;
    public TextMeshProUGUI scoreText;
    public MotivationalMessageManager motivationalMessageManager;  // Reference to the new message manager
    private int score = 0;

    void Start()
    {
        UpdateScore(0);
    }

    public void PlayerReachedGoal()
    {
        score += 10;
        UpdateScore(score);

        // Display a motivational message using the manager
        if (motivationalMessageManager != null)
        {
            motivationalMessageManager.DisplayMotivationalMessage();
        }

        player.RestartGame();
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    public void PlayerFellOff()
    {
        score = Mathf.Max(0, score - 5);
        UpdateScore(score);
        player.RestartGame();
    }
}
