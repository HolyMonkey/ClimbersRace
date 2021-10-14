using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private void Awake()
    {
        BackgroundMusic[] loadedObjects = FindObjectsOfType<BackgroundMusic>();
        if (loadedObjects.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}