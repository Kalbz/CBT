using TMPro;
using UnityEngine;

public class PlayerRopeController : MonoBehaviour
{
    public Transform startPoint; // Start Point
    public Transform goalPoint; // End Point (finish line)
    public RespirationBar respirationBar; // Reference to the breathing bar
    public TextMeshProUGUI messageText; // Motivational messages
    public TextMeshProUGUI barText; // Instructions about respiration
    public GameObject endGamePanel; // Reference to the End Game Panel
    public TextMeshProUGUI scoreText; // Reference to the score text in the panel
    public string[] motivationalMessages = new string[]
            {
         "Don't give up, you can do it!",
         "Try again, success is close!",
         "Breathe deeply and try again!"
            };

    private Rigidbody rb;
    private bool isFalling = false;
    private int fallCount = 0; // Tracks the number of falls

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        ResetPlayerPosition();
       
        // Ensure the end game panel is inactive at start
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }
    }

    private void Update()
    {
        // managing the instructions for the respiration of the player
        if (respirationBar.IsIncreasing())
        {
            barText.text = "Breathe in..";
        }
        else
        {
            barText.text = "Breathe out!";
        }
    }

    // Called by the UI button
    public void MovePlayerButton()
    {
        if (isFalling) return;

        // Check if the bar is increasing
        if (respirationBar.IsIncreasing())
        {
            MoveForward(); // The player only walks if the bar is increasing
        }
        else
        {
            fallCount++; // Increment the fall counter
            FallOffRope(); // Falls if the bar is decreasing
        }
    }

    void MoveForward()
    {
        rb.useGravity = false;
        rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * 18f);
    }

    public void FallOffRope()
    {
        rb.useGravity = true;

        isFalling = true;

        // Temporarily disables player control (removes velocity and rotation)
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Calculate a random direction (left or right) for the falling movement
        Vector3 fallDirection = Random.value > 0.5f ? Vector3.right : Vector3.left;

        // Apply a force in the random direction and downwards
        rb.AddForce((fallDirection + Vector3.down) * 5f, ForceMode.Impulse);

        // Display a motivational message
        int randomIndex = Random.Range(0, motivationalMessages.Length);
        messageText.text = motivationalMessages[randomIndex];

        // Restore the player after falling
        Respawn();
    }

    private void Respawn()
    {
        // Wait 2 seconds before resetting the position
        Invoke(nameof(ResetPlayerPosition), 2f);
    }

    private void ResetPlayerPosition()
    {
        // Reset the position and rotation 
        transform.position = startPoint.position; // Set the exact position of the start
        transform.rotation = Quaternion.identity; // Reset the rotation

        // Reset the Rigidbody and velocities
        rb.velocity = Vector3.zero; // Set the linear velocity to zero
        rb.angularVelocity = Vector3.zero; // Set the angular velocity to zero

        // Reset the falling state
        isFalling = false;
    }

    private void ShowEndGamePanel()
    {
        if (endGamePanel != null)
        {
            // Calculate the score based on falls
            int maxScore = 100;
            int penaltyPerFall = 10; // Lose 10 points per fall
            int score = Mathf.Max(maxScore - (fallCount * penaltyPerFall), 0);

            // Update the UI
            endGamePanel.SetActive(true);
            messageText.text = ""; // Clear any motivational messages
            scoreText.text = $"Congratulations!\nYour Score: {score}/100";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fall"))
        {
            FallOffRope();
        }
        else if (other.CompareTag("Goal"))
        {
            ShowEndGamePanel();
        }
        else if (other.CompareTag("Bird"))
        {
            fallCount++; // Increment the fall counter
            Debug.Log("You hit the bird");
            FallOffRope(); // The player falls if he hits the bird
        }
    }
}