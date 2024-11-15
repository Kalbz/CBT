using UnityEngine;
using TMPro;

public class MotivationalMessageManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;  // Reference to the UI Text element to display the message
    private string[] motivationalMessages = {
        "Focus and positive thinking help you overcome any tension.",
        "Keep your balance and move forward, don't give up!",
        "Every step is an achievement. Keep going!",
        "You are capable of more than you know.",
        "Stay strong, you're doing amazing work!",
        "This journey is tough, but so are you."
    };

    public void DisplayMotivationalMessage()
    {
        if (messageText != null)
        {
            messageText.text = motivationalMessages[Random.Range(0, motivationalMessages.Length)];
            StartCoroutine(HideMessageAfterDelay(5f));  // Hide the message after 3 seconds
        }
        else
        {
            Debug.LogWarning("Message Text is not assigned!");
        }
    }

    private System.Collections.IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";  // Hide the message by setting the text to empty
    }

}
