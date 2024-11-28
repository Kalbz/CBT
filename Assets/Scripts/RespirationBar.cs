using UnityEngine;
using UnityEngine.UI;

public class RespirationBar : MonoBehaviour
{
    public Image barFill; // "Fill" part of the bar
    public float cycleDuration = 4f; // Duration of a complete breathing cycle (in seconds)

    private float timer = 0f;
    private float previousBarValue = 0f; // Last value of the bar
    private bool isIncreasing = true; // Indicates whether the bar is increasing

    void Update()
    {
        if (barFill == null) return;

        // Update the timer and calculate the phase of the breathing
        timer += Time.deltaTime;
        float phase = Mathf.PingPong(timer / cycleDuration, 1f);

        // Update the bar
        barFill.fillAmount = phase;

        // Calculate the direction of the bar
        isIncreasing = phase > previousBarValue;
        previousBarValue = phase;
    }

    public bool IsIncreasing()
    {

        // Returns true if the bar is increasing
        return isIncreasing;
    }

    public float GetBarValue()
    {
        // Returns the current value of the bar (between 0 and 1)
        return barFill.fillAmount;
    }
}