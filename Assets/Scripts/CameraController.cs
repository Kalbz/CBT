using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // Initial offset to start above and behind the target
    [SerializeField] private float rotationSpeed = 0.2f; // Sensitivity of the camera rotation
    [SerializeField] private float zoomSpeed = 5.0f; // Speed of zooming
    [SerializeField] private float minZoom = 3.0f;   // Minimum distance to target
    [SerializeField] private float maxZoom = 15.0f;  // Maximum distance to target
    [SerializeField] private float minVerticalAngle = 5.0f; // Minimum allowable vertical angle to prevent looking under the ground
    [SerializeField] private float maxVerticalAngle = 80.0f; // Maximum allowable vertical angle to prevent looking too far up

    private float currentVerticalAngle = 20.0f; // Start at a safe angle above the target
    private Vector3 lastMousePosition;

    void Start()
    {
        // Set the initial camera position
        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Handle zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            float distance = offset.magnitude;
            distance = Mathf.Clamp(distance - scroll * zoomSpeed, minZoom, maxZoom);
            offset = offset.normalized * distance;
        }

        // Rotate based on mouse movement
        Vector3 currentMousePosition = Input.mousePosition;

        if (lastMousePosition != Vector3.zero)
        {
            Vector3 deltaMousePosition = currentMousePosition - lastMousePosition;

            // Horizontal rotation
            float mouseX = deltaMousePosition.x * rotationSpeed;
            Quaternion horizontalRotation = Quaternion.AngleAxis(mouseX, Vector3.up);
            offset = horizontalRotation * offset;

            // Vertical rotation with clamping
            float mouseY = deltaMousePosition.y * rotationSpeed;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle - mouseY, minVerticalAngle, maxVerticalAngle);
            
            // Update vertical rotation based on clamped angle
            Vector3 direction = offset.normalized;
            direction.y = Mathf.Tan(currentVerticalAngle * Mathf.Deg2Rad) * Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z);
            offset = direction * offset.magnitude;
        }

        // Update last mouse position
        lastMousePosition = currentMousePosition;

        // Follow the player
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }

    private void OnDisable()
    {
        // Reset last mouse position
        lastMousePosition = Vector3.zero;
    }
}
