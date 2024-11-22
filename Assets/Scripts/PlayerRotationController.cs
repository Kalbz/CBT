using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    public Transform character; // El transform del personaje
    public Slider horizontalSlider; // Slider para el movimiento horizontal
    public Slider verticalSlider; // Slider para el movimiento vertical
    public float maxVerticalAngle = 30f; // Límite superior del ángulo vertical
    public float minVerticalAngle = 0f; // Límite inferior del ángulo vertical

    void Update()
    {
        // Obtener los valores de los sliders
        float horizontalValue = horizontalSlider.value; // Rotación en el eje Y
        float verticalValue = verticalSlider.value; // Rotación en el eje X

        // Limitar la rotación vertical dentro del rango permitido
        verticalValue = Mathf.Clamp(verticalValue, minVerticalAngle, maxVerticalAngle);

        // Aplicar la rotación al personaje
        character.localEulerAngles = new Vector3(-verticalValue, horizontalValue, 0);
    }
}
