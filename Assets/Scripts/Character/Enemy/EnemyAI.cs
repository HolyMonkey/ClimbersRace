using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Character _enemyCharacter;
    [SerializeField] private float _choseNextBalkTimer;
    [SerializeField] private RangeFloat _dragTimeRange;

    private EnemyBalk _currentBalk => (EnemyBalk)_enemyCharacter.CurrentBalk;
    private EnemyBalk _nextBalk;
    private EnemyBalk _prevBalk;

    private float _timer = 0;

    private Vector3 _dragVector = new Vector3();
    private float _dragTime;
    private Coroutine _draggingBalkJob;

    //private void OnEnable()
    //{
    //    _enemyCharacter.AttachingBalk += OnAttachingBalk;
    //}

    //private void OnDisable()
    //{
    //    _enemyCharacter.AttachingBalk -= OnAttachingBalk;
    //}

    //private void OnAttachingBalk(Balk arg0)
    //{
    //    throw new NotImplementedException();
    //}


    private void Update()//можно переписать на события
    {
        if (_enemyCharacter.IsAttachingBalk)
        {
            if (!_nextBalk)
            {
                _timer += Time.deltaTime;

                if (_timer > _choseNextBalkTimer)
                {
                    _timer = 0;

                    while (!_nextBalk || _nextBalk == _prevBalk)
                        _nextBalk = ChooseNextBalk(_currentBalk);

                    ConfigureDragParameters();
                }
            }
            else if (_draggingBalkJob == null)
            {
                _draggingBalkJob = StartCoroutine(DraggingBalk());
            }
        }
    }

    private EnemyBalk ChooseNextBalk(EnemyBalk currentBalk)
    {
        return currentBalk.GetRandomHigherBalk();
    }

    private void ConfigureDragParameters()
    {
        _dragVector = (_currentBalk.transform.position - _nextBalk.transform.position).normalized;
        _dragTime = _dragTimeRange.RandomValue;
    }

    private IEnumerator DraggingBalk()
    {
        _currentBalk.BalkMovement.BeginDragBalk();

        float timer = 0;
        float dragValue = 0f;

        while (timer < _dragTime)
        {
            timer += Time.deltaTime;
            _currentBalk.BalkMovement.DragBalk(_dragVector, dragValue / _dragTime);

            yield return null;
        }

        _prevBalk = _currentBalk;
        _currentBalk.BalkMovement.FinishDragBalk(); // => _currentBalk == null
        _nextBalk = null;

        _draggingBalkJob = null;
    }
}
