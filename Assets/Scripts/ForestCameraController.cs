using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestCameraController : MonoBehaviour
{
    public Transform player; 
    private Vector3 offset; // distance between camera and player

    void Start()
    {
        // calculates the offset 
        if (player != null)
        {
            offset = transform.position - player.position;
        }
        else
        {
            Debug.LogError("Player reference is not set in the script.");
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // keeping the camera at the inizial distance
            transform.position = player.position + offset;
        }
    }
}
