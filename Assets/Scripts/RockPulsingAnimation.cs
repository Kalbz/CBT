using UnityEngine;

public class EmissionController : MonoBehaviour
{
    private Renderer targetRenderer;
    public float emissionIntensity = 1.0f;
    public Color emissionColor = Color.blue;

    void Start()
    {
        // Get the Renderer component automatically
        targetRenderer = GetComponent<Renderer>();

        if (targetRenderer == null)
        {
            Debug.LogError("Renderer not found on " + gameObject.name);
            return;
        }

        // Initialize emission with the specified intensity and color
        SetEmission(emissionIntensity);
    }

    public void SetEmission(float intensity)
    {
        if (targetRenderer != null)
        {
            // Update the emission color and intensity
            Color finalColor = emissionColor * Mathf.LinearToGammaSpace(intensity);
            targetRenderer.material.SetColor("_EmissionColor", finalColor);
        }
    }

    void Update()
    {
        float pulse = Mathf.PingPong(Time.time, 1.2f) * emissionIntensity;
        SetEmission(pulse);
    }
}
