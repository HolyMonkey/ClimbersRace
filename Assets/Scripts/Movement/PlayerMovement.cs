using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementHandler _movement;

    private Vector3 _basePosition;

    public event UnityAction<Vector3> KnockedDownEnemy;

    private void Awake()
    {
        _basePosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MovementHandler enemy))
        {
            if (enemy.CatchedOnBalk)
            {
                KnockedDownEnemy?.Invoke(transform.position);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerBalk playerBalk) /*&& _movement.CatchedOnBalk == false*/)
        {
            _movement.AttachToBalk(playerBalk);
        }
    }

    [ContextMenu(nameof(ResetPosition))]
    private void ResetPosition()
    {
        transform.position = _basePosition;
    }
}
