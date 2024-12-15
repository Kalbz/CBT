using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Resetting PlayerPrefs for a new session.");
        PlayerPrefs.DeleteAll(); // Clears all stored PlayerPrefs data
        PlayerPrefs.Save(); // Ensures the data is cleared immediately
    }
}
