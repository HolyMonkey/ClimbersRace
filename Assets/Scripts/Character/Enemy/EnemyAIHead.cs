using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHead : MonoBehaviour
{
    [SerializeField] private List<EnemyAI> _enemiesAI;
    [SerializeField] private Level _level;

    private void OnEnable()
    {
        _level.LevelStarted += OnLevelStarted;
        _level.LevelWon += OnGameWon;
        _level.LevelLost += OnLoseGame;
        _level.BonusGameStarted += OnBonusStarted;

        StopAIs();
    }

    private void OnDisable()
    {
        _level.LevelStarted -= OnLevelStarted;
        _level.LevelWon -= OnGameWon;
        _level.LevelLost -= OnLoseGame;
        _level.BonusGameStarted -= OnBonusStarted;
    }

    private void OnBonusStarted()
    {
        StopAIs();
    }

    private void OnGameWon()
    {
        StopAIs();
    }

    private void OnLevelStarted()
    {
        StartAIs();
    }

    private void OnLoseGame()
    {
        StopAIs();
    }

    private void StartAIs()
    {
        foreach (EnemyAI enemyAI in _enemiesAI)
            enemyAI.StartAI();
    }

    private void StopAIs()
    {
        foreach (EnemyAI enemyAI in _enemiesAI)
            enemyAI.StopAI();
    }
}
