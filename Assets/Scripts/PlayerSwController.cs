using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerwController : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody del personaje
    public Slider horizontalSlider; // Slider para rotación horizontal
    public Slider verticalSlider; // Slider para rotación vertical
    public Button jumpButton; // Botón para salto

    public float maxVerticalAngle = 30f; // Máximo ángulo para mirar hacia arriba
    public float minVerticalAngle = 0f; // Mínimo ángulo para mirar hacia adelante
    public float maxJumpForce = 10f; // Fuerza máxima del salto
    public float chargeSpeed = 5f; // Velocidad de carga del salto

    private float currentJumpForce = 0f; // Fuerza actual del salto
    private bool isCharging = false; // Si se está cargando el salto

    void Start()
    {
        // Configurar los eventos del botón de salto
        jumpButton.onClick.AddListener(StartCharge);
        jumpButton.onClick.AddListener(ReleaseJump);
    }

    void Update()
    {
        // Rotación del personaje basada en los sliders
        float horizontalValue = horizontalSlider.value;
        float verticalValue = verticalSlider.value;

        // Limitar la rotación vertical
        verticalValue = Mathf.Clamp(verticalValue, minVerticalAngle, maxVerticalAngle);

        // Aplicar la rotación
        transform.localEulerAngles = new Vector3(-verticalValue, horizontalValue, 0);

        // Cargar el salto mientras se mantiene presionado
        if (isCharging)
        {
            currentJumpForce += chargeSpeed * Time.deltaTime;
            currentJumpForce = Mathf.Clamp(currentJumpForce, 0, maxJumpForce);
        }
    }

    void StartCharge()
    {
        isCharging = true; // Inicia la carga
    }

    void ReleaseJump()
    {
        if (isCharging)
        {
            // Salto hacia adelante y arriba
            Vector3 jumpDirection = transform.forward + Vector3.up;
            rb.AddForce(jumpDirection.normalized * currentJumpForce, ForceMode.Impulse);

            // Reiniciar carga
            currentJumpForce = 0f;
            isCharging = false;
        }
    }
}
