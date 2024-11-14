using UnityEngine;

public class RockHealth : MonoBehaviour
{
    public int health = 10;  // Set the initial health of the rock
    public float knockbackForce = 5f;  // Force applied to the rock when hit

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the rock!");
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
            Destroy(gameObject);  // Destroy the rock when health reaches zero
        }
    }
}
