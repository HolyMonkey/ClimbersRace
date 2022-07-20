using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agava.YandexGames;
using Agava.YandexGames.Utility;
using AnimatedUI;
using UnityEngine.Events;
using System;

public class Advertisement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerScore;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;
    [SerializeField] private string _leaderboardName = "LeaderBoard";
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

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        _money = FindObjectOfType<Money>();
        _level = FindObjectOfType<Level>();
        //WebBackgroundMute.Enabled = true;
    }

    private void OnEnable()
    {
        VideoOpened += OnVideoOpened;
        VideoRewarded += OnVideoRewarded;
        VideoClosed += OnVideoClosed;
        InterestialOpened += OnInterestialOpened;
        InterestialClosed += OnInterestialClosed;
    }

    private void OnDisable()
    {
        VideoOpened -= OnVideoOpened;
        VideoRewarded -= OnVideoRewarded;
        VideoClosed -= OnVideoClosed;
        InterestialOpened -= OnInterestialOpened;
        InterestialClosed -= OnInterestialClosed;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.WaitForInitialization();

        OnRequestPersonalProfileDataPermissionButtonClick();
    }

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
}