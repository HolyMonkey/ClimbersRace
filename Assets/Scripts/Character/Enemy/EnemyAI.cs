using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Character _enemyCharacter;
    [SerializeField] private RangeFloat _choosingBalkDelayRange;
    [SerializeField] private RangeFloat _dragTimeRange;
    [SerializeField] private BalkInput _playerStarted;

    private BalkAINode _currentNode;
    private BalkAINode _nextNode;
    private BalkAINode _prevNode;

    private Vector3 _dragVector = new Vector3();
    private float _dragTime;
    private float _averageSpeed = 7f;
    private bool _isPlayerStarted = false;

    private Coroutine _choosingJob;
    private Coroutine _draggingBalkJob;

    private void OnEnable()
    {
        _playerStarted.PlayerStartMoved += OnPlayerStarted;
        _enemyCharacter.AttachingBalk += OnAttachingBalk;
        _enemyCharacter.DetachingBalk += OnDetachingBalk;
    }

    private void OnDisable()
    {
        _playerStarted.PlayerStartMoved -= OnPlayerStarted;
        _enemyCharacter.AttachingBalk -= OnAttachingBalk;
        _enemyCharacter.DetachingBalk -= OnDetachingBalk;

        CheckAndStopCoroutine(ref _draggingBalkJob);
    }

    public void SetCurrentNode(BalkAINode balkAINode)
    {
        _currentNode = balkAINode;
    }

    private void OnPlayerStarted()
    {
        _isPlayerStarted = true;
    }

    public void StartAI()
    {
        enabled = true;

        _choosingJob = StartCoroutine(ChoosingNextNode());
        _draggingBalkJob = StartCoroutine(DraggingBalk(_choosingJob));
    }

    public void StopAI()
    {
        enabled = false;

        CheckAndStopCoroutine(ref _choosingJob);
        CheckAndStopCoroutine(ref _draggingBalkJob);
    }

    private void OnAttachingBalk(Balk balk)
    {
        CheckAndStopCoroutine(ref _choosingJob);
        _choosingJob = StartCoroutine(ChoosingNextNode());

        CheckAndStopCoroutine(ref _draggingBalkJob);
        _draggingBalkJob = StartCoroutine(DraggingBalk(_choosingJob));
    }

    private void OnDetachingBalk()
    {
        CheckAndStopCoroutine(ref _choosingJob);
        CheckAndStopCoroutine(ref _draggingBalkJob);

        _prevNode = _currentNode;
        _nextNode = null;
    }

    private IEnumerator ChoosingNextNode()
    {
        float choosingBalkDelay = _choosingBalkDelayRange.RandomValue;

        yield return new WaitForSeconds(choosingBalkDelay);

        ChooseNextNode();
    }

    private void ChooseNextNode()
    {
        if (_currentNode.NearNodesCount == 0)
        {
            Debug.LogError(name + " no near nodes, enemy turn away");
            _nextNode = _prevNode;
            return;
        }

        BalkAINode targetNode = _currentNode.GetRandomHigherNode() != null ? _currentNode.GetRandomHigherNode() : _currentNode.GetRandomNode();

        if (_currentNode.NearNodesCount == 1)
        {
            _nextNode = targetNode;
            return;
        }
        else if (_currentNode.NearNodesCount >= 2)
        {
            if (_prevNode != null && targetNode == _prevNode)
            {
                if (_currentNode.HigherNodesCount <= 1)
                {
                    targetNode = _currentNode.GetRandomNode();
                    if (targetNode == _prevNode) //double chance for non-prev
                        targetNode = _currentNode.GetRandomNode();
                    _nextNode = targetNode;
                    return;
                }
                else
                {
                    ChooseNextNode();
                    return;
                }
            }
            else
            {
                _nextNode = targetNode;
                return;
            }
        }
    }

    private IEnumerator DraggingBalk(Coroutine choosingJob)
    {
        yield return new WaitUntil(() => _isPlayerStarted == true);
        if (choosingJob != null)
            yield return choosingJob;

        ConfigureDragParameters();

        _currentNode.BalkMovement.BeginDragBalk();

        float timer = 0;
        while (timer < _dragTime)
        {
            timer += Time.deltaTime;
            _currentNode.BalkMovement.DragBalk(_dragVector, timer / _dragTime);

            yield return null;
        }

        _currentNode.BalkMovement.FinishDragBalk(); // => OnDetaching, wait for attaching

        _draggingBalkJob = null;
    }

    private void ConfigureDragParameters()
    {
        _dragTime = _dragTimeRange.RandomValue;

        Vector3 betweenBalkVector = _nextNode.transform.position - _currentNode.transform.position;
        _dragVector = -betweenBalkVector;

        float t = betweenBalkVector.magnitude / _averageSpeed;
        float yPrecision = Physics.gravity.y * t * t / 2;
        _dragVector.y += yPrecision;

        _dragVector.Normalize();
    }

    private void CheckAndStopCoroutine(ref Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
