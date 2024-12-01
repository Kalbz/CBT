using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float speed = 2f; // Bird speed
    public float moveRange = 4f; // Distance the bird flyes in both directions

    private Vector3 startPosition;
    private int direction = 1; // Initial direction (1 is for right and -1 is for left)

    void Start()
    {
        startPosition = transform.position; // Saving initial position
    }

    void Update()
    {
        // Moves the bird
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        // Limits
        if (Vector3.Distance(startPosition, transform.position) >= moveRange)
        {
            transform.Rotate(0f, 180f, 0f); // Rotate bird
            direction *= -1; // Change direction
        }
    }
}
