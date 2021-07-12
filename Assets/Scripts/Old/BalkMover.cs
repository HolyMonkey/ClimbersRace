using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkMover : MonoBehaviour
{
    [SerializeField] private Collider _balkCollider;
    [SerializeField] private float _maxDragDistance;
    [SerializeField] private float _timeToLeaveBalk;

    private PlayerMovement _playerMover;
    private Vector3 _startDragPosition;
    private Vector3 _offset;
    private float _zOffset;

    private void Start()
    {
        _zOffset = Camera.main.WorldToScreenPoint(transform.localPosition).z;
    }

    private void OnTriggerStay(Collider other)
    {
        if(_playerMover == null)
        {
            if (other.TryGetComponent(out PlayerMovement playerMover))
            {
                _playerMover = playerMover;
            }
        }
    }

    private void OnMouseDown()
    {
        if(_playerMover != null)
        {
            //_playerMover.Movement.SetStartDragPosition(transform.position);

            _offset = transform.position - GetMousePosition();
            _startDragPosition = transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (_playerMover != null)
        {
            Vector3 newPosition = GetMousePosition() + _offset;
            Vector3 offset = newPosition - _startDragPosition;

            transform.position = _startDragPosition + Vector3.ClampMagnitude(offset, _maxDragDistance);
        }
    }

    private void OnMouseUp()
    {
        if (_playerMover != null)
        {
            Vector3 endDragPosition = transform.position;
            Vector3 direction = _startDragPosition - endDragPosition;

            //_playerMover.Movement.DetachFromBalk();
            //_playerMover.Movement.Push(direction.normalized);
            _playerMover = null;

            StartCoroutine(DisableColliderForWhile(_timeToLeaveBalk));
        }        
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zOffset;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private IEnumerator DisableColliderForWhile(float delay)
    {
        _balkCollider.enabled = false;
        
        yield return new WaitForSeconds(delay);

        _balkCollider.enabled = true;
    }
}
