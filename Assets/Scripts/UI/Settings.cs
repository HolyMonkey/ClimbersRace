using Agava.YandexGames.Utility;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames.Utility;

public class Settings : MonoBehaviour
{
    private const string AUDIO = "AudioSettings";
    private const string VIBRATION = "VibrationSettings";

    [SerializeField] private Image _audioIcon;
    [SerializeField] private Image _vibrationIcon;
    [SerializeField] private Sprite _audioOn;
    [SerializeField] private Sprite _audioOff;
    [SerializeField] private Sprite _vibrationOn;
    [SerializeField] private Sprite _vibrationOff;
    [SerializeField] private CanvasGroup _settingsPanel;

    private bool _isAudioOn = true;
    private bool _isVibrationOn = true;

    private void Awake()
    {
        Load();
        ApplySettings();
        Render(GetCurrentAudioSprite(), GetCurrentVibrationSprite());
    }

    private void Update()
    {
<<<<<<< HEAD
        AudioListener.volume = !WebApplication.InBackground && _isAudioOn ? 1 : 0;
    }

    public void SetAudioSetting(bool isAudioOn)
    {
        _isAudioOn = isAudioOn;
        ApplySettings();
        Render(GetCurrentAudioSprite(), GetCurrentVibrationSprite());
=======
        AudioListener.volume = WebApplication.InBackground || !_isAudioOn ? 0 : 1;
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
    }

    public void ChangeAudioSetting()
    {
        _isAudioOn = !_isAudioOn;
        Save();
        ApplySettings();
        Render(GetCurrentAudioSprite(), GetCurrentVibrationSprite());
    }

    public void SetAudioSetting(bool value)
    {
        _isAudioOn = value;
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
        _settingsPanel.alpha = 1;
        _settingsPanel.blocksRaycasts = true;
        _settingsPanel.interactable = true;
    }

    public void HideSettings()
    {
        _settingsPanel.alpha = 0;
        _settingsPanel.blocksRaycasts = false;
        _settingsPanel.interactable = false;
    }

    private void ApplySettings()
    {
        AudioListener.volume = _isAudioOn ? 1 : 0;

        Debug.Log("audio:" + _isAudioOn + "  vibration:" + _isVibrationOn);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(AUDIO, _isAudioOn ? 1 : 0);
        PlayerPrefs.SetInt(VIBRATION, _isVibrationOn ? 1 : 0);
    }

    public void Load()
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

    public void ChangeVisibilitySetting()
    {
        if (_settingsPanel.alpha == 1)
            HideSettings();
        else
            ShowSettings();
    }
}