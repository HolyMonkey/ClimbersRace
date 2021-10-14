using System;
using UnityEngine;

public class AmplitudeAnalytics : MonoBehaviour
{
    private const string SAVED_REG_DAY = "RegDaySave";
    private const string SAVED_REG_DAY_FULL = "RegDaySaveFull";
    private const string SAVED_SESSION_ID = "SessionID";

    private Level _level;
    private Money _money;

    private Amplitude _amplitude;

    private string _regDay
    {
        get { return PlayerPrefs.GetString(SAVED_REG_DAY, DateTime.Today.ToString("dd/MM/yy")); }
        set { PlayerPrefs.SetString(SAVED_REG_DAY, value); }
    }

    private string _regDayFull
    {
        get { return PlayerPrefs.GetString(SAVED_REG_DAY_FULL, DateTime.Today.ToString()); }
        set { PlayerPrefs.SetString(SAVED_REG_DAY_FULL, value); }
    }

    private int _sessionID
    {
        get { return PlayerPrefs.GetInt(SAVED_SESSION_ID, 0); }
        set { PlayerPrefs.SetInt(SAVED_SESSION_ID, value); }
    }

    private void Awake()
    {
        _level = GetComponent<Level>();
        _money = FindObjectOfType<Money>();

        _amplitude = Amplitude.Instance;
        _amplitude.logging = true;
        _amplitude.init("4e761091a725a4a0cbcddbc5fa9915b9");
    }

    private void OnEnable()
    {
        _level.LevelStarted += OnLevelStarted;
        _level.LevelWon += OnLevelWon;
        _level.LevelLost += OnlevelLost;
    }

    private void OnDisable()
    {
        _level.LevelStarted -= OnLevelStarted;
        _level.LevelWon -= OnLevelWon;
        _level.LevelLost -= OnlevelLost;
    }

    public void GameStart()
    {
        if (_level.CurrentLevel == 1)
        {
            _regDay = DateTime.Today.ToString("dd/MM/yy");
            _regDayFull = DateTime.Today.ToString();
            _amplitude.setOnceUserProperty("reg_day", _regDay);
        }

        SetBasicProperty();
        FireEvent("game_start");
    }

    private void OnlevelLost() => FireEvent("level_fail");

    private void OnLevelWon()
    {
        FireEvent("level_win");
    }

    private void OnLevelStarted()
    {
        FireEvent("level_start");
    }

    private void SetBasicProperty()
    {
        _sessionID = _sessionID + 1;
        _amplitude.setUserProperty("session_id", _sessionID);

        int daysAfter = DateTime.Today.Subtract(DateTime.Parse(_regDayFull)).Days;
        _amplitude.setUserProperty("days_after", daysAfter);
    }

    private void FireEvent(string eventName)
    {
        SettingUserProperties();
        _amplitude.logEvent(eventName);
    }

    private void SettingUserProperties()
    {
        int currentMoney = _money.CurrentMoney;
        _amplitude.setUserProperty("current_soft", currentMoney);

        int lastLevel = _level.CurrentLevel;
        _amplitude.setUserProperty("level_last", lastLevel);
    }

}
