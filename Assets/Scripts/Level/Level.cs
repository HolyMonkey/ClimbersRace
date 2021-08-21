using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    private const string CURRENT_LEVEL_ID = "CurrentLevelID";

    [SerializeField] private BonusGame _bonusGame;
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Character _playerCharacter;
    [SerializeField] private CameraMover _cameraMover;

    public int CurrentLevel => PlayerPrefs.GetInt(CURRENT_LEVEL_ID, 1);

    public event UnityAction LevelStarted;
    public event UnityAction BonusGameStarted;
    public event UnityAction LevelWon;
    public event UnityAction LevelLost;

    private bool _isLevelStarted = false;

    private void OnEnable()
    {
        _playerCharacter.Dying += OnPlayerDying;
    }

    private void OnDisable()
    {
        _playerCharacter.Dying -= OnPlayerDying;
    }

    private void OnPlayerDying(Character playerCharacter)
    {
        LoseGame();
    }

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

    public void StartBonusGame(FinishBalk finishBalk, Character character)
    {
        if (character == _playerCharacter)
        {
            _bonusGame.StartBonusGame(finishBalk);
            BonusGameStarted?.Invoke();
        }
        else
        {
            LoseGame();
            _cameraMover.ChangeTarget(character);
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);

        _sceneChanger.LoadLevel(CurrentLevel);
    }

    public void RestartLevel()
    {
        _sceneChanger.LoadLevel(CurrentLevel);
    }

    public void StartLevel()
    {
        LevelStarted?.Invoke();
    }

    public void WinGame()
    {
        LevelWon?.Invoke();
    }

    public void LoseGame()
    {
        LevelLost?.Invoke();
    }
}