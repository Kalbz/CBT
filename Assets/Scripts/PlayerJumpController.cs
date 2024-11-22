using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerJumpController : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody del personaje
    public float maxJumpForce = 30f; // Fuerza máxima del salto
    public float chargeSpeed = 5f; // Velocidad de carga del salto

    private float currentJumpForce = 0f; // Fuerza actual del salto
    private bool _isCharging = false; // Cambia el nombre aquí

    public void StartCharge()
    {
        _isCharging = true;
        Debug.Log("Charging jump...");
    }

    public void ReleaseJump()
    {
        if (_isCharging) // Cambia todas las referencias a _isCharging
        {
            // Aplicar la fuerza al personaje
            Vector3 jumpDirection = transform.forward + Vector3.up;
            rb.AddForce(jumpDirection.normalized * currentJumpForce, ForceMode.Impulse);

            // Reiniciar valores
            currentJumpForce = 0f;
            _isCharging = false;

            Debug.Log("Jump executed!");
        }
    }

    void Update()
    {
        if (_isCharging) // Cambia aquí también
        {
            currentJumpForce += chargeSpeed * Time.deltaTime;
            currentJumpForce = Mathf.Clamp(currentJumpForce, 0, maxJumpForce);
        }
    }
}
