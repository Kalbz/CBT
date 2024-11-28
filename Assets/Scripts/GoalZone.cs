using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRopeController player = other.GetComponent<PlayerRopeController>();
            if (player != null)
            {
                player.messageText.text = "Congratulations! You won!";
            }
        }
    }
}
