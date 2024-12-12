using UnityEngine;

public class PickaxeCollision : MonoBehaviour
{
    public int damage = 1;  // Amount of damage to deal to the rock
    private PickaxeSwing pickaxeSwing;
    private float damageCooldown = 0.3f; // Time between each damage application in seconds
    private float lastDamageTime = 0f; // Keeps track of the last time damage was applied


    void Start()
    {
        // Reference the PickaxeSwing script on the same GameObject
        pickaxeSwing = GetComponent<PickaxeSwing>();
        if (pickaxeSwing == null)
        {
            Debug.LogError("PickaxeSwing script not found on the pickaxe GameObject!");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // Check if the pickaxe head is still in contact with the rock
        if (other.CompareTag("Rock"))
        {
            Debug.Log($"Pickaxe still in contact with: {other.gameObject.name}");

            if (pickaxeSwing != null && pickaxeSwing.IsSwinging)
            {
                // Check if enough time has passed since the last damage application
                if (Time.time - lastDamageTime >= damageCooldown)
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

                        // Update the last damage time
                        lastDamageTime = Time.time;
                    }
                    else
                    {
                        Debug.LogWarning("No RockHealth script found on the collided rock!");
                    }
                }
                else
                {
                    Debug.Log("Damage cooldown in effect, no damage applied.");
                }
            }
            else
            {
                Debug.Log("Pickaxe is NOT swinging, no damage applied.");
            }
        }
    }

}
