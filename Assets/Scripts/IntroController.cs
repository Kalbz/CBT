using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public GameObject introPanel; // Reference to the Panel
    public CanvasGroup canvasGroup; // Add a CanvasGroup to the Panel for fading

    public void StartGame()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 1f; // Duration of the fade
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            yield return null;
        }

        introPanel.SetActive(false);
    }
}


