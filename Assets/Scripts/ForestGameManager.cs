using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForestGameManager : MonoBehaviour
{
    public PlayerRopeController player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI messageText;
    private int score = 0;
    private string[] motivationalMessages = {
        "Focus and positive thinking help you overcome any tension.",
        "Keep your balance and move forward, don't give up!",
        "Every step is an achievement. Keep going!"
    };

    void Start()
    {
        UpdateScore(0);
    }

    public void PlayerReachedGoal()
    {
        score += 10;
        UpdateScore(score);
        DisplayMotivationalMessage();
        player.RestartGame();
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    private void DisplayMotivationalMessage()
    {
        messageText.text = motivationalMessages[Random.Range(0, motivationalMessages.Length)];
    }

    public void PlayerFellOff()
    {
        score = Mathf.Max(0, score - 5);
        UpdateScore(score);
        player.RestartGame();
    }
}
