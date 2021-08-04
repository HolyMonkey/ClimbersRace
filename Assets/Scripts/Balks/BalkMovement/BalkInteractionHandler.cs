using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkInteractionHandler : MonoBehaviour
{
    [SerializeField] private Collider _balkCollider;
    [SerializeField] private float _maxDragDistance;
    [SerializeField] private float _timeToLeaveBalk;

    private Character _character;
    private Vector3 _startDragPosition;

    public void BeginDragBalk()
    {
        if (_character != null)
        {
            _character.SetStartDragPosition();
            _startDragPosition = transform.position;
        }
    }

    public void DragBalk(Vector3 currentPosition)
    {
        if (_character != null)
        {
            Vector3 offset = currentPosition - _startDragPosition;

            transform.position = _startDragPosition + Vector3.ClampMagnitude(offset, _maxDragDistance);
        }
    }

    public void FinishDragBalk()
    {
        if (_character != null)
        {
            Vector3 endDragPosition = transform.position;
            Vector3 direction = _startDragPosition - endDragPosition;

            _character.DetachFromBalk();
            _character.Push(direction.normalized);
            _character = null;

            StartCoroutine(DisableColliderOnTime(_timeToLeaveBalk));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            if (_character != null && _character != character)
            {
                _character.CollideWithTrap();
            }

            _character = character;
        }
    }

    private IEnumerator DisableColliderOnTime(float delay)
    {
        _balkCollider.enabled = false;

        yield return new WaitForSeconds(delay);

        _balkCollider.enabled = true;
    }
}
