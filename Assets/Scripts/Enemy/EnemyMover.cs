using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


[RequireComponent(typeof(Movement))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private GameWall _gameWall;
    [SerializeField] private Transform[] _wayPoints;

    private Movement _movement;
    private Sequence _sequence;

    private float _timeOfDragging = 0.75f, _dragDelay = 1.5f;
    private int _currentWaypointNumber = 0;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _sequence = DOTween.Sequence();
    }

    private void Start()
    {
        _sequence.Append(transform.DOMove(GetTargetPoint(), _timeOfDragging).SetDelay(_dragDelay));
    }

    private void FixedUpdate()
    {
        _movement.ClampMexSpeed();

        if (transform.localPosition == GetTargetPoint())
        {
            _movement.DetatchFromHook();
            _currentWaypointNumber++;
            _sequence.Append(transform.DOMove(GetTargetPoint(), _timeOfDragging).SetDelay(_dragDelay));
        }
    }

    private Vector3 GetTargetPoint()
    {
        return _wayPoints[_currentWaypointNumber].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyBalk enemyBalk))
        {
            _movement.AttachToHook(enemyBalk);
            if (_movement.Rigidbody.velocity.magnitude > _movement.MaxAttachingSpeed)
            {
                StartCoroutine(_movement.SwingHook(0.5f));
            }
        }
    }
}
