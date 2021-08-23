using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private AudioSource _currentMusic;

    private void Awake()
    {
        _currentMusic = GetComponent<AudioSource>();

        BackgroundMusic[] loadedObjects = FindObjectsOfType<BackgroundMusic>();
        if (loadedObjects.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}