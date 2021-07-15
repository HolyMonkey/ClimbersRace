using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkMovementHandler : MonoBehaviour
{
    [SerializeField] private Collider _balkCollider;
    [SerializeField] private float _maxDragDistance;
    [SerializeField] private float _timeToLeaveBalk;

    private MovementHandler _movement;
    private Vector3 _startDragPosition;

    public void BeginDragBalk()
    {
        if (_movement != null)
        {
            _movement.SetStartDragPosition(transform.position);
            _startDragPosition = transform.position;
        }
    }

    public void DragBalk(Vector3 currentPosition)
    {
        if (_movement != null)
        {
            Vector3 offset = currentPosition - _startDragPosition;

            transform.position = _startDragPosition + Vector3.ClampMagnitude(offset, _maxDragDistance);
        }
    }

    public void FinishDragBalk()
    {
        if (_movement != null)
        {
            Vector3 endDragPosition = transform.position;
            Vector3 direction = _startDragPosition - endDragPosition;
            direction.y = Mathf.Abs(direction.y);

            _movement.DetachFromBalk();
            _movement.Push(direction.normalized);
            _movement = null;

            StartCoroutine(DisableColliderForWhile(_timeToLeaveBalk));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out MovementHandler movement))
        {
            if (_movement != null && _movement != movement)
            {
                _movement.CollideWithTrap();
            }

            _movement = movement;
        }
    }

    private IEnumerator DisableColliderForWhile(float delay)
    {
        _balkCollider.enabled = false;

        yield return new WaitForSeconds(delay);

        _balkCollider.enabled = true;
    }
}
