using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private const string AUDIO = "AudioSettings";
    private const string VIBRATION = "VibrationSettings";

    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Image _audioIcon;
    [SerializeField] private Image _vibrationIcon;
    [SerializeField] private Sprite _audioOn;
    [SerializeField] private Sprite _audioOff;
    [SerializeField] private Sprite _vibrationOn;
    [SerializeField] private Sprite _vibrationOff;

    private bool _isAudioOn = true;
    private bool _isVibrationOn = true;

    private void Start()
    {
        Load();
        ApplySettings();
        Render(GetCurrentAudioSprite(), GetCurrentVibrationSprite());
    }

    public void ChangeAudioSetting()
    {
        _isAudioOn = !_isAudioOn;
        Save();
        ApplySettings();
        Render(GetCurrentAudioSprite(), GetCurrentVibrationSprite());
    }

    public void ChangeVibrationSetting()
    {
        _isVibrationOn = !_isVibrationOn;
        Save();
        ApplySettings();
        Render(GetCurrentAudioSprite(), GetCurrentVibrationSprite());
    }

    public void ShowSettings()
    {
        _settingsPanel.gameObject.SetActive(true);
    }

    public void HideSettings()
    {
        _settingsPanel.gameObject.SetActive(false);
    }

    private void ApplySettings()
    {
        AudioListener.volume = _isAudioOn ? 1 : 0;
        MMVibrationManager.SetHapticsActive(_isVibrationOn);

        Debug.Log("audio:" + _isAudioOn + "  vibration:" + _isVibrationOn);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(AUDIO, _isAudioOn ? 1 : 0);
        PlayerPrefs.SetInt(VIBRATION, _isVibrationOn ? 1 : 0);
    }

    private void Load()
    {
        _isAudioOn = PlayerPrefs.GetInt(AUDIO, 1) == 1;
        _isVibrationOn = PlayerPrefs.GetInt(VIBRATION, 1) == 1;

        Render(GetCurrentAudioSprite(), GetCurrentVibrationSprite());
    }

    private void Render(Sprite audioSprite, Sprite vibrationSprite)
    {
        _audioIcon.sprite = audioSprite;
        _vibrationIcon.sprite = vibrationSprite;
    }

    private Sprite GetCurrentAudioSprite()
    {
        return _isAudioOn ? _audioOn : _audioOff;
    }
    private Sprite GetCurrentVibrationSprite()
    {
        return _isVibrationOn ? _vibrationOn : _vibrationOff;
    }
}