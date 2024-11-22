using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Transform lastStone; // Referencia a la última piedra en la que estuvo
    public Transform respawnPoint; // Punto de respawn
    public Rigidbody rb; // Rigidbody del personaje

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Stone"))
        {
            lastStone = collision.transform; // Actualiza la última piedra
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GameManager.Instance.SubtractScore(1); // Resta 1 al score
            Respawn(); // Respawnea al jugador
        }
    }

    void Respawn()
    {
        if (lastStone != null)
        {
            transform.position = lastStone.position + Vector3.up * 2f; // Reaparece encima de la última piedra
            rb.velocity = Vector3.zero; // Reinicia la velocidad del Rigidbody
        }
        else
        {
            transform.position = respawnPoint.position; // Reaparece en el punto de inicio
        }
    }
}
