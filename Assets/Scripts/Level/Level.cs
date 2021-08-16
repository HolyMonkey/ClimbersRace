using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    private const string CURRENT_LEVEL_ID = "CurrentLevelID";

    [SerializeField] private BonusGame _bonusGame;
    [SerializeField] private SceneChanger _sceneChanger;

    public int CurrentLevel => PlayerPrefs.GetInt(CURRENT_LEVEL_ID, 1);

    public event UnityAction LevelStarted;
    public event UnityAction BonusGameStarted;
    public event UnityAction GameWon;
    public event UnityAction GameLost;

    private bool _isLevelStarted = false;

    private void Update()
    {
        if (_isLevelStarted)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            StartLevel();
            _isLevelStarted = true;
        }
    }

    public void StartBonusGame(FinishBalk finishBalk)
    {
        _bonusGame.StartBonusGame(finishBalk);
        BonusGameStarted?.Invoke();
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);

        _sceneChanger.LoadLevel(CurrentLevel);
    }

    public void StartLevel()
    {
        LevelStarted?.Invoke();
    }

    public void WinGame()
    {
        GameWon?.Invoke();
    }

    public void LoseGame()
    {
        GameLost?.Invoke();
    }
}