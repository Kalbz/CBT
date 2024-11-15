using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update() // Reverted back to Update from FixedUpdate
    {
        // Check if the player is on the ground
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Back to original value
        }

        // Get the input from player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get direction relative to the camera forward and right vectors
        Vector3 move = cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput;

        // Eliminate vertical component to prevent unwanted vertical movement
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Rotate the player to face the movement direction (revert to instant rotation)
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move; // Rotate player instantly towards the movement direction
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
