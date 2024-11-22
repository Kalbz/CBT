using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton para referencia global
    private int score = 0; // Score del jugador

    void Awake()
    {
        // Configurar el Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    public void SubtractScore(int amount)
    {
        score -= amount;
        Debug.Log("Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }
}
