using MoreMountains.Feedbacks;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Money : MonoBehaviour
{
    private const string SAVED_MONEY = "MoneySaveID";
    [SerializeField] private MMFeedbacks _collectCoinFeedbacks;

    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int, int> LevelIncomeReady;

    private int _currentLevelMoney = 0;
    private int _currentMultiplier = 1;

    public int CurrentMoney
    {
        get { return PlayerPrefs.GetInt(SAVED_MONEY, 0); }
        private set { PlayerPrefs.SetInt(SAVED_MONEY, value); }
    }

    public void PullEvent()
    {
        MoneyChanged?.Invoke(CurrentMoney);
    }

    public void AddLevelMoney(int value)
    {
        if (value < 0)
            throw new ArgumentException();

        _collectCoinFeedbacks?.PlayFeedbacks();

        ChangeBalance(value);

        CountLevelMoney(value);
    }

    public void RecieveLevelBonus()
    {
        ChangeBalance(_currentLevelMoney * _currentMultiplier);

        _currentLevelMoney = 0;
        _currentMultiplier = 1;
    }

    public bool TryRemoveMoney(int value)
    {
        if (value < 0)
            throw new ArgumentException();

        if (CurrentMoney >= value)
        {
            ChangeBalance(-value);
            return true;
        }
        else
            return false;
    }

    public void SetLevelBonusMultiplier(int multiplier)
    {
        if (multiplier < 1)
            throw new ArgumentException();

        _currentMultiplier = multiplier;
        LevelIncomeReady?.Invoke(_currentLevelMoney, _currentMultiplier);
    }

    private void ChangeBalance(int value)
    {
        CurrentMoney += value;
        PullEvent();
    }

    private void CountLevelMoney(int value)
    {
        _currentLevelMoney += value;
    }
}
