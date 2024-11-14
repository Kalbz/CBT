using UnityEngine;

public class PickaxeSwing : MonoBehaviour
{
    public Transform pickaxe;          // Reference to the pickaxe transform
    public float swingAngle = 45f;     // The angle of the swing
    public float swingSpeed = 10f;     // Speed of swinging back and forth

    private Quaternion initialRotation;
    private bool isSwinging = false;

    // Property to check the swing status
    public bool IsSwinging => isSwinging;

    void Start()
    {
        // Store the initial rotation of the pickaxe
        initialRotation = pickaxe.localRotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            // Start swinging
            StartCoroutine(SwingPickaxe());
        }
    }

    System.Collections.IEnumerator SwingPickaxe()
    {
        isSwinging = true;

        // Rotate to the swing angle along the Z-axis
        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles + new Vector3(0, 0, swingAngle));
        float elapsedTime = 0f;

        // Swing to the target rotation
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * swingSpeed;
            pickaxe.localRotation = Quaternion.Slerp(initialRotation, targetRotation, Mathf.PingPong(elapsedTime, 1f));
            yield return null;
        }

        // Return to initial position
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * swingSpeed;
            pickaxe.localRotation = Quaternion.Slerp(targetRotation, initialRotation, Mathf.PingPong(elapsedTime, 1f));
            yield return null;
        }

        // Reset rotation
        pickaxe.localRotation = initialRotation;

        isSwinging = false;
    }
}
