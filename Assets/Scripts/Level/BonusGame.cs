using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGame : MonoBehaviour
{
    [SerializeField] private BonusScaleView _bonusScaleView;
    [SerializeField] private GameFinish _gameFinish;

    [SerializeField] private float _speed;

    [SerializeField] private List<BonusWall> _bonusWallsMultipliers;
    [Range(0f, 1f)]
    [SerializeField] private List<float> _valueToMultiplierBounds;

    [SerializeField] private AnimationCurve _playerYCurve;

    private FinishBalk _targetBalk;

    private bool _isBonusScaling = false;

    private float _time = 0f;

    private void OnValidate()
    {
        if (_valueToMultiplierBounds.Count != _bonusWallsMultipliers.Count)
            Debug.LogError("Bonus Values doesnt match with bonus Walls");
    }

    private void Update()
    {
        if (!_isBonusScaling)
            return;

        float value = 0f;
        UpdateScaleValue(out value);

        _targetBalk.DragFinishBalk(value);
        _bonusScaleView.ChangeValue(value);


        if (Input.GetMouseButtonDown(0))
        {
            FinishBonusGame(value);
        }
    }

    public void StartBonusGame(FinishBalk finishBalk)
    {
        _targetBalk = finishBalk;
        _bonusScaleView.Show();
        _gameFinish.RotateFinishWall(finishBalk.transform.position);

        _time = 0f;
        _isBonusScaling = true;
    }

    private void FinishBonusGame(float endValue)
    {
        _isBonusScaling = false;
        _bonusScaleView.Hide();

        int reachedBonusLevel = 0;
        for (int i = 0; i < _valueToMultiplierBounds.Count; i++)
        {
            if (endValue > _valueToMultiplierBounds[i])
                reachedBonusLevel = i;
        }

        BonusWall targetWall = _bonusWallsMultipliers[reachedBonusLevel];

        _targetBalk.FinishPush(targetWall, _playerYCurve);
    }

    private void UpdateScaleValue(out float value)
    {
        _time += Time.deltaTime * _speed;
        value = Mathf.Lerp(0f, 1f, Mathf.PingPong(_time, 1f));

        value = Mathf.Clamp(value, 0f, 1f);
    }
}
