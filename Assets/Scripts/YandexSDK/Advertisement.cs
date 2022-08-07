using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agava.YandexGames;
using Agava.YandexGames.Utility;
<<<<<<< HEAD
=======
using AnimatedUI;
using UnityEngine.Events;
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
using System;

public class Advertisement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerScore;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;
    [SerializeField] private string _leaderboardName = "LeaderBoard";
<<<<<<< HEAD
    [SerializeField] private int _adMultiplier;
    [SerializeField] private Settings _settings;

    private Level _level;
    private Money _money;

    public int AdMultiplier => _adMultiplier;

    public event Action AdOpened;
    public event Action AdRewarded;
    public event Action AdClosed;
    public event Action<string> AdError;
=======
    [SerializeField] private CanvasFade _logInPanel;
    [SerializeField] private CanvasFade _leaderboardPanel;
    [SerializeField] private int _adBonusMultiplier = 2;
    [SerializeField] private Settings _settings;

    private Money _money;
    private Level _level;

    public int AdBonusMultiplier => _adBonusMultiplier;

    public event Action VideoOpened;
    public event Action VideoRewarded;
    public event Action VideoClosed;
    public event Action<string> VideoError;

    public event Action InterestialOpened;
    public event Action InterestialOffline;
    public event Action<bool> InterestialClosed;
    public event Action<string> InterestialError;
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
<<<<<<< HEAD
=======
        _money = FindObjectOfType<Money>();
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
        _level = FindObjectOfType<Level>();
        //WebBackgroundMute.Enabled = true;
    }

    private void OnEnable()
    {
<<<<<<< HEAD
        AdClosed += OnAdClosed;
        _level.LevelLost += ShowInterestial;
=======
        VideoOpened += OnVideoOpened;
        VideoRewarded += OnVideoRewarded;
        VideoClosed += OnVideoClosed;
        InterestialOpened += OnInterestialOpened;
        InterestialClosed += OnInterestialClosed;
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
    }

    private void OnDisable()
    {
<<<<<<< HEAD
        AdClosed -= OnAdClosed;
        _level.LevelLost -= ShowInterestial;
=======
        VideoOpened -= OnVideoOpened;
        VideoRewarded -= OnVideoRewarded;
        VideoClosed -= OnVideoClosed;
        InterestialOpened -= OnInterestialOpened;
        InterestialClosed -= OnInterestialClosed;
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.WaitForInitialization();

        OnRequestPersonalProfileDataPermissionButtonClick();
    }

<<<<<<< HEAD
    private void OnAdClosed()
    {
        _settings.Load();
        _level.NextLevel();
    }

    public void ShowInterestial()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterestialAd.Show();
#endif
    }

    public void ShowVideo()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(AdOpened, AdRewarded, AdClosed, AdError);
=======
    private void OnVideoOpened()
    {
        _settings.SetAudioSetting(false);
    }

    private void OnVideoRewarded()
    {
        _money.SetLevelBonusMultiplier(_money.CurrentMultiplier * _adBonusMultiplier);
    }

    private void OnVideoClosed()
    {
        _settings.Load();
        _money.RecieveLevelBonus();
        _level.NextLevel();
        
    }

    private void OnInterestialOpened()
    {
        _settings.SetAudioSetting(false);
    }

    private void OnInterestialClosed(bool value)
    {
        _settings.Load();
    }

    public void OnShowInterestial()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            InterestialAd.Show(InterestialOpened, InterestialClosed, InterestialError, InterestialOffline);
#endif
    }

    public void OnShowVideo()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show(VideoOpened, VideoRewarded, VideoClosed, VideoError);
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
#endif
    }

    public void OnAuthorize()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.Authorize();
#endif
    }

    public void OnRequestPersonalProfileDataPermissionButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.RequestPersonalProfileDataPermission();
#endif
    }

    public void OnSetLeaderboardScoreButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.SetScore(_leaderboardName, _money.CurrentMoney);
#endif
<<<<<<< HEAD
    }

    public void GetLeaderboardEntries()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.GetEntries(_leaderboardName, (result) =>
            {
                int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;
                for (int i = 0; i < leadersNumber; i++)
                {
                    string name = result.entries[i].player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = "Анонимно";

                    _leaderNames[i].text = name;
                    _scoreList[i].text = result.entries[i].formattedScore;
                }
            });
#endif
    }
=======
    }

    public void OnGetLeaderboardEntriesButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            _leaderboardPanel.Show();
            int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;
            for (int i = 0; i < leadersNumber; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Анонимно";

                _leaderNames[i].text = name;
                _scoreList[i].text = result.entries[i].formattedScore;
            }
        },
        (error) =>
        {
            _logInPanel.Show();
        });
#endif
    }
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
}