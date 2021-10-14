using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Character _characterInteractionHandler;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _timeOfDragging = 0.75f;
    [SerializeField] private float _dragDelay = 1.5f;

    private int _currentWaypointNumber = 0;
    private Tweener _tweener;
    private bool _isSlidingDown;

    public Vector3 NextTargetPosition => _wayPoints[_currentWaypointNumber - 1].position;

    private void OnEnable()
    {
        _characterInteractionHandler.Falling += OnSlidingDown;
    }

    private void OnDisable()
    {
        _characterInteractionHandler.Falling -= OnSlidingDown;
    }

    private void Start()
    {
        MoveToNextPosition();
    }

    public void MoveToNextPosition()
    {
        if (_isSlidingDown == false)
        {
            if (_tweener == null || _tweener.IsComplete())
            {
                Vector3 targetPosition = _wayPoints[_currentWaypointNumber].position;

                if (_currentWaypointNumber + 1 < _wayPoints.Length)
                {
                    _currentWaypointNumber++;
                }

                _tweener = transform.DOMove(targetPosition, _timeOfDragging).SetDelay(_dragDelay).SetAutoKill(false);
                _tweener.Restart();
            }
        }
    }

    private void OnSlidingDown()
    {
        _isSlidingDown = true;
        _tweener.Kill();
    }
}
