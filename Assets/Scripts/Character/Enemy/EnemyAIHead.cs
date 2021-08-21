using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHead : MonoBehaviour
{
    [SerializeField] private List<EnemyAI> _enemiesAI;
    [SerializeField] private Level _level;

    private void OnEnable()
    {
        _level.LevelStarted += OnLevelStarted;
        _level.GameWon += OnGameWon;
        _level.GameLost += OnLoseGame;
        _level.BonusGameStarted += OnBonusStarted;

        EnemyAIEnabled(false);
    }

    private void OnDisable()
    {
        _level.LevelStarted -= OnLevelStarted;
        _level.GameWon -= OnGameWon;
        _level.GameLost -= OnLoseGame;
        _level.BonusGameStarted -= OnBonusStarted;
    }

    private void OnBonusStarted()
    {
        EnemyAIEnabled(false);
    }

    private void OnGameWon()
    {
        EnemyAIEnabled(false);
    }

    private void OnLevelStarted()
    {
        EnemyAIEnabled(true);
    }

    private void OnLoseGame()
    {
        EnemyAIEnabled(false);
    }

    private void EnemyAIEnabled(bool isEnabled)
    {
        foreach (EnemyAI enemyAI in _enemiesAI)
        {
            enemyAI.enabled = isEnabled;
        }
    }
}
