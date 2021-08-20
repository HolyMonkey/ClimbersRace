using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Character _enemyCharacter;
    [SerializeField] private float _choseNextBalkTimer;
    [SerializeField] private RangeFloat _dragTimeRange;
    [SerializeField] private Level _level;

    public BalkAINode CurrentNode;
    private BalkAINode _nextNode;
    private BalkAINode _prevNode;

    private Vector3 _dragVector = new Vector3();
    private float _dragTime;
    private Coroutine _draggingBalkJob;

    private bool _isLevelStarted = false;
    private float _timer = 0;

    private void OnEnable()
    {
        _level.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _level.LevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        _isLevelStarted = true;
    }

    private void Update()//ìîæíî ïåðåïèñàòü íà ñîáûòèÿ
    {
        if (_isLevelStarted && _enemyCharacter.IsAttachingBalk)
        {
            if (!_nextNode)
            {
                _timer += Time.deltaTime;

                if (_timer > _choseNextBalkTimer)
                {
                    _timer = 0;

                    int cycleCount = 0;
                    while (!_nextNode || _nextNode == _prevNode)
                    {
                        _nextNode = ChooseNextNode(CurrentNode);

                        if (CurrentNode.NearBalksCount == 0)
                        {
                            Debug.LogError(name + " no available balks, enemy turn away");
                            _nextNode = _prevNode;
                            _prevNode = CurrentNode;
                            break;
                        }

                        if (_nextNode == _prevNode && CurrentNode.NearBalksCount < 2)
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

    private BalkAINode ChooseNextNode(BalkAINode currentNode)
    {
        if (currentNode.GetRandomHigherNode())
            return currentNode.GetRandomHigherNode();
        else
            return currentNode.GetRandomNode();
    }

    private void ConfigureDragParameters()
    {
        _dragTime = _dragTimeRange.RandomValue;

        Vector3 betweenBalkVector = _nextNode.transform.position - CurrentNode.transform.position;
        _dragVector = -betweenBalkVector;

        float t = betweenBalkVector.magnitude / 7f; //average speed
        float yPrecision = Physics.gravity.y * t * t / 2;
        _dragVector.y += yPrecision;

        _dragVector.Normalize();
    }

    private IEnumerator DraggingBalk()
    {
        CurrentNode.BalkMovement.BeginDragBalk();

        float timer = 0;

        while (timer < _dragTime)
        {
            timer += Time.deltaTime;
            CurrentNode.BalkMovement.DragBalk(_dragVector, timer / _dragTime);

            yield return null;
        }

        _prevNode = CurrentNode;
        CurrentNode.BalkMovement.FinishDragBalk(); // => _currentBalk == null, wait for attaching
        _nextNode = null;

        _draggingBalkJob = null;
    }
}
