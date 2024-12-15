using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

void Awake()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("AudioManager set to DontDestroyOnLoad");
    }
    else
    {
        Debug.Log("Duplicate AudioManager destroyed");
        Destroy(gameObject);
    }
}

}
