using UnityEngine;

public class RockHealth : MonoBehaviour
{
    public int health = 3;  // Set the initial health of the rock
    public float knockbackForce = 2f;  // Force applied to the rock when hit
    private Rigidbody rb;
    private MotivationalMessageManager motivationalMessageManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the rock!");
        }

        // Find the MotivationalMessageManager in the scene
        motivationalMessageManager = FindObjectOfType<MotivationalMessageManager>();
        if (motivationalMessageManager == null)
        {
            Debug.LogError("MotivationalMessageManager not found in the scene!");
        }
    }

    public void TakeDamage(int damage, Vector3 hitDirection)
    {
        health -= damage;
        Debug.Log($"Rock {gameObject.name} took {damage} damage. Remaining health: {health}");

        // Apply knockback force if Rigidbody is present
        if (rb != null)
        {
            rb.AddForce(hitDirection * knockbackForce, ForceMode.Impulse);
            Debug.Log($"Rock {gameObject.name} knocked back!");
        }

        if (health <= 0)
        {
            Debug.Log($"Rock {gameObject.name} destroyed.");

            // Show a motivational message when the rock is destroyed
            if (motivationalMessageManager != null)
            {
                motivationalMessageManager.DisplayMotivationalMessage();
            }

            Destroy(gameObject);  // Destroy the rock when health reaches zero
        }
    }
}
