using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    [SerializeField] private Transform _water;
    [SerializeField] private Transform _deathCollider;
    [SerializeField] private float _increasingWaterSpeed;
    [SerializeField] private Level _level;

    private bool _isWaterIncreasing = false;

    private void OnEnable()
    {
        _level.LevelStarted += OnLevelStarted;
        _level.BonusGameStarted += OnBonusStarted;
        _level.LevelLost += OnGameLost;
    }

    private void OnDisable()
    {
        _level.LevelStarted -= OnLevelStarted;
        _level.BonusGameStarted -= OnBonusStarted;
        _level.LevelLost -= OnGameLost;
    }

    private void OnGameLost()
    {
        _isWaterIncreasing = false;
    }

    private void OnLevelStarted()
    {
        _isWaterIncreasing = true;
    }

    private void OnBonusStarted()
    {
        _isWaterIncreasing = false;
    }

    private void Update()
    {
        if (_isWaterIncreasing)
        {
            Vector3 movement = Vector3.up * Time.deltaTime * _increasingWaterSpeed;
            _water.position += movement;
            _deathCollider.position += movement;
        }
    }
}
