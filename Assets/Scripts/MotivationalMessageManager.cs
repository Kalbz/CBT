using UnityEngine;
using TMPro;
using System.Collections;

public class MotivationalMessageManager : MonoBehaviour
{
    public GameObject thinkingBubble;             // Reference to the existing bubble GameObject
    public Transform playerTransform;             // Reference to the player's Transform
    public Vector3 offset = new Vector3(-2f, 2.5f, 0);  // Offset from the player's position

    private Animator bubbleAnimator;              // Animator for the bubble
    private TextMeshProUGUI messageText;          // Text component for displaying messages

    private string[] motivationalMessages = {
        "Focus and positive thinking help you overcome any tension.",
        "Keep your balance and move forward, don't give up!",
        "Every step is an achievement. Keep going!",
        "You are capable of more than you know.",
        "Stay strong, you're doing amazing work!",
        "This journey is tough, but so are you."
    };

    void Start()
    {
        // Assign the TextMeshProUGUI component
        messageText = thinkingBubble.GetComponentInChildren<TextMeshProUGUI>();
        if (messageText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found in ThinkingBubble!");
        }

        // Assign the Animator
        bubbleAnimator = thinkingBubble.GetComponent<Animator>();
        if (bubbleAnimator == null)
        {
            Debug.LogWarning("Animator not found on ThinkingBubble GameObject!");
        }

        // Ensure the bubble is initially hidden
        thinkingBubble.SetActive(false);
    }

    void Update()
    {
        // Keep the thinking bubble at an offset position relative to the player
        if (thinkingBubble != null && playerTransform != null && thinkingBubble.activeSelf)
        {
            thinkingBubble.transform.position = playerTransform.position + offset;
        }
    }

    public void DisplayMotivationalMessage()
    {
        if (thinkingBubble != null && messageText != null && bubbleAnimator != null)
        {
            // Set a random motivational message
            messageText.text = motivationalMessages[Random.Range(0, motivationalMessages.Length)];
            messageText.alpha = 0; // Make the text invisible initially

            // Show the thinking bubble
            thinkingBubble.SetActive(true);

            // Play the animation
            bubbleAnimator.Play("Idle", -1, 0f);
            bubbleAnimator.SetTrigger("BubbleAnimation");

            // Start the fade-in for text
            StartCoroutine(FadeInText(messageText, 2f, 0.5f)); // 1-second delay, 0.5-second fade-in duration

            // Hide the bubble after a delay
            StartCoroutine(HideBubbleAfterDelay(5f)); // Example: total display time 5 seconds
        }
        else
        {
            Debug.LogWarning("ThinkingBubble, MessageText, or Animator not assigned!");
        }
    }

    private IEnumerator FadeInText(TextMeshProUGUI text, float delay, float duration)
    {
        yield return new WaitForSeconds(delay); // Wait for the delay before starting fade-in

        float elapsed = 0f; // Initialize elapsed time
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // Increment elapsed time
            float alpha = Mathf.Clamp01(elapsed / duration); // Calculate alpha value
            text.alpha = alpha; // Apply alpha to text
            yield return null; // Wait for the next frame
        }
        text.alpha = 1f; // Ensure text is fully visible at the end
    }

    private IEnumerator HideBubbleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Trigger fade-out animation
        if (bubbleAnimator != null)
        {
            bubbleAnimator.SetTrigger("BubbleAnimation2");
            yield return new WaitForSeconds(1f); // Wait for fade-out animation to complete
        }

        // Hide the thinking bubble
        thinkingBubble.SetActive(false);
        if (messageText != null)
        {
            messageText.alpha = 0; // Reset text opacity
        }
    }
}
