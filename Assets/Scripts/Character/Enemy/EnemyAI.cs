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

    private Vector3 _dragVector = new Vector3();
    private float _dragTime;
    private Coroutine _draggingBalkJob;

    private float _timer = 0;

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


    private void Update()//ìîæíî ïåðåïèñàòü íà ñîáûòèÿ
    {
        if (_enemyCharacter.IsAttachingBalk)
        {
            if (!_nextBalk)
            {
                _timer += Time.deltaTime;

                if (_timer > _choseNextBalkTimer)
                {
                    _timer = 0;

                    int cycleCount = 0;
                    while (!_nextBalk || _nextBalk == _prevBalk)
                    {
                        _nextBalk = ChooseNextBalk(_currentBalk);

                        if (_currentBalk.NearBalksCount == 0)
                        {
                            Debug.LogError(name + " no available balks, enemy turn away");
                            _nextBalk = _prevBalk;
                            _prevBalk = _currentBalk;
                            break;
                        }

                        if (_nextBalk == _prevBalk && _currentBalk.NearBalksCount < 2)
                        {
                            Debug.LogError(name + " no non-previous balk");
                            enabled = false;
                            return;
                        }

                        cycleCount++;
                        if (cycleCount > 10)
                        {
                            Debug.LogError(name + " ñhooseNextBalk cycle error");
                            enabled = false;
                            return;
                        }
                    }

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
        if (currentBalk.GetRandomHigherBalk())
            return currentBalk.GetRandomHigherBalk();
        else
            return currentBalk.GetRandomBalk();
    }

    private void ConfigureDragParameters()
    {
        _dragTime = _dragTimeRange.RandomValue;

        Vector3 betweenBalkVector = _nextBalk.transform.position - _currentBalk.transform.position;
        _dragVector = -betweenBalkVector;

        float t = betweenBalkVector.magnitude / 7f; //average speed
        float yPrecision = Physics.gravity.y * t * t / 2;
        _dragVector.y += yPrecision;

        _dragVector.Normalize();
    }

    private IEnumerator DraggingBalk()
    {
        _currentBalk.BalkMovement.BeginDragBalk();

        float timer = 0;

        while (timer < _dragTime)
        {
            timer += Time.deltaTime;
            _currentBalk.BalkMovement.DragBalk(_dragVector, timer / _dragTime);

            yield return null;
        }

        _prevBalk = _currentBalk;
        _currentBalk.BalkMovement.FinishDragBalk(); // => _currentBalk == null, wait for attaching
        _nextBalk = null;

        _draggingBalkJob = null;
    }
}
