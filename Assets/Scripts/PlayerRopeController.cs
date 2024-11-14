using UnityEngine;

public class PlayerRopeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float balanceSensitivity = 2f;
    public float maxTiltAngle = 15f;
    private bool isBalancing = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents automatic rotation from Rigidbody physics
    }

    void Update()
    {
        // Move forward
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, move);

        // Balance control
        if (isBalancing)
        {
            float tilt = Input.GetAxis("Horizontal") * balanceSensitivity;
            float newTilt = Mathf.Clamp(transform.eulerAngles.z + tilt, -maxTiltAngle, maxTiltAngle);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newTilt);

            // Check if the player has tilted too far and falls off
            if (Mathf.Abs(newTilt) >= maxTiltAngle)
            {
                FallOff();
            }
        }
    }

    void FallOff()
    {
        isBalancing = false;
        rb.useGravity = true;
        Debug.Log("You fell! Try again.");
    }

    public void RestartGame()
    {
        transform.position = new Vector3(34, 6, 0); // Starting position
        transform.eulerAngles = Vector3.zero;
        isBalancing = true;
        rb.useGravity = false;
        Debug.Log("Restarting the attempt.");
    }
}
