using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehavior : MonoBehaviour
{
    public float vibrationDuration = 0.5f; // Duración de la vibración
    public float vibrationIntensity = 0.1f; // Intensidad de la vibración
    private Vector3 originalPosition; // Para guardar la posición original de la piedra

    private bool isSteppedOn = false; // Si el jugador ya ha usado esta piedra

    void Start()
    {
        originalPosition = transform.position; // Guardar posición inicial
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSteppedOn)
        {
            isSteppedOn = true; // Marca que la piedra fue usada
            GameManager.Instance.AddScore(2); // Suma 2 puntos al score
            StartCoroutine(Vibrate()); // Inicia vibración
        }
    }

    IEnumerator Vibrate()
    {
        float elapsedTime = 0f;

        while (elapsedTime < vibrationDuration)
        {
            transform.position = originalPosition + Random.insideUnitSphere * vibrationIntensity; // Vibra
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition; // Restaurar posición
        gameObject.SetActive(false); // Desaparece la piedra
    }
}
