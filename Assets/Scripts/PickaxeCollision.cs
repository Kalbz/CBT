using UnityEngine;

public class PickaxeCollision : MonoBehaviour
{
    public int damage = 1;  // Amount of damage to deal to the rock
    private PickaxeSwing pickaxeSwing;

    void Start()
    {
        // Reference the PickaxeSwing script on the same GameObject
        pickaxeSwing = GetComponent<PickaxeSwing>();
        if (pickaxeSwing == null)
        {
            Debug.LogError("PickaxeSwing script not found on the pickaxe GameObject!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the pickaxe head collided with a rock
        if (other.CompareTag("Rock"))
        {
            Debug.Log($"Pickaxe hit: {other.gameObject.name}");

            if (pickaxeSwing != null && pickaxeSwing.IsSwinging)
            {
                Debug.Log("Pickaxe is swinging, applying damage.");

                RockHealth rockHealth = other.GetComponent<RockHealth>();
                if (rockHealth != null)
                {
                    // Calculate the direction from the pickaxe to the rock
                    Vector3 hitDirection = (other.transform.position - transform.position).normalized;

                    // Apply damage with knockback
                    rockHealth.TakeDamage(damage, hitDirection);
                    Debug.Log($"Damage applied to rock: {other.gameObject.name}");
                }
                else
                {
                    Debug.LogWarning("No RockHealth script found on the collided rock!");
                }
            }
            else
            {
                Debug.Log("Pickaxe is NOT swinging, no damage applied.");
            }
        }
    }
}
