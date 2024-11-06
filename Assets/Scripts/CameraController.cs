using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7);
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float rotationSpeed = 5.0f;

    private bool isFreeCameraActive = false;

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetKey(KeyCode.K))
        {
            // Activate free camera mode
            isFreeCameraActive = true;

            // Get the mouse movement inputs
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotate the camera based on mouse movement
            transform.RotateAround(target.position, Vector3.up, mouseX * rotationSpeed);
            transform.RotateAround(target.position, transform.right, -mouseY * rotationSpeed);

            // Update the offset based on the new camera position
            offset = transform.position - target.position;
        }
        else
        {
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
