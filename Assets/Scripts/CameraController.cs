using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7);
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float rotationSpeed = 5.0f;

    private bool isFreeCameraActive = false;
    private Vector3 lastMousePosition;

    void LateUpdate()
    {
        if (target == null) return;

        // Check if the middle mouse button is pressed down
        if (Input.GetMouseButtonDown(2))
        {
            // Activate free camera mode and record initial mouse position
            isFreeCameraActive = true;
            lastMousePosition = Input.mousePosition;
        }

        // Check if the middle mouse button is being held down
        if (Input.GetMouseButton(2) && isFreeCameraActive)
        {
            // Get the current mouse position and calculate the delta
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 deltaMousePosition = currentMousePosition - lastMousePosition;

            // Rotate the camera based on mouse movement
            float mouseX = deltaMousePosition.x * rotationSpeed * Time.deltaTime;
            float mouseY = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            transform.RotateAround(target.position, Vector3.up, mouseX);
            transform.RotateAround(target.position, transform.right, -mouseY);

            // Immediately update offset and position for real-time following
            offset = transform.position - target.position;
            lastMousePosition = currentMousePosition;
        }

        // Regular follow mode when the middle mouse button is not held
        if (!Input.GetMouseButton(2))
        {
            isFreeCameraActive = false;

            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate between current position and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the camera position
            transform.position = smoothedPosition;

            // Make the camera look at the target
            transform.LookAt(target);
        }
    }
}
