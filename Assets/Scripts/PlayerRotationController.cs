using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    public Transform character; // El transform del personaje
    public Slider horizontalSlider; // Slider para el movimiento horizontal
    public Slider verticalSlider; // Slider para el movimiento vertical
    public float maxVerticalAngle = 30f; // L�mite superior del �ngulo vertical
    public float minVerticalAngle = 0f; // L�mite inferior del �ngulo vertical

    void Update()
    {
        // Obtener los valores de los sliders
        float horizontalValue = horizontalSlider.value; // Rotaci�n en el eje Y
        float verticalValue = verticalSlider.value; // Rotaci�n en el eje X

        // Limitar la rotaci�n vertical dentro del rango permitido
        verticalValue = Mathf.Clamp(verticalValue, minVerticalAngle, maxVerticalAngle);

        // Aplicar la rotaci�n al personaje
        character.localEulerAngles = new Vector3(-verticalValue, horizontalValue, 0);
    }
}
