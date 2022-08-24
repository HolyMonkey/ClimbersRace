using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackMusic : MonoBehaviour
{
    [SerializeField] private LoadScreen _loadScreen;

    private const string CURRENT_MUSIC_STATE = "CurrentMusicIsState";

    private bool _isBlockChangeMusic = false;
    private AudioSource _audio;

    public int CurrentMusicState => PlayerPrefs.GetInt(CURRENT_MUSIC_STATE, 0);

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        BackMusic[] loadedObjects = FindObjectsOfType<BackMusic>();
        for (int i = 0; i < loadedObjects.Length; i++)
        {
            if (i == 0)
                _audio = loadedObjects[i].GetComponent<AudioSource>();
            else
                Destroy(gameObject);
        }

        _isBlockChangeMusic = false;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if(_loadScreen)
            _loadScreen.ProggresBarFinished += OnStop;
    }

    public void Play()
    {
        if (_isBlockChangeMusic == false)
        {
            if (CurrentMusicState == 0)
            {
                _audio.Play();
                PlayerPrefs.SetInt(CURRENT_MUSIC_STATE, +1);
            }
        }
    }

    public void OnStop()
    {
        if (CurrentMusicState == 1)
        {
            _audio.Stop();
            PlayerPrefs.SetInt(CURRENT_MUSIC_STATE, 0);
        }
            
        _loadScreen.ProggresBarFinished -= OnStop;
    }

    public void OnBlockChangeMusic()
    {
        _isBlockChangeMusic = true;
    }
}
