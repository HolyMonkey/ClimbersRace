using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();   
    }

    private void Start()
    {

        //BackgroundMusic[] loadedObjects = FindObjectsOfType<BackgroundMusic>();
        //if (loadedObjects.Length > 1)
        //    Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }
}