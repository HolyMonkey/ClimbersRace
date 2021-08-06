using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Balk))]
public class BalkInteractionHandler : MonoBehaviour
{
    [SerializeField] private Collider _balkCollider;
    [SerializeField] private float _maxDragDistance;
    [SerializeField] private float _timeToLeaveBalk;

    private Balk _balk;
    private Vector3 _startDragPosition;

    private void Awake()
    {
        _balk = GetComponent<Balk>();
    }

    public void BeginDragBalk()
    {
        if (_balk.CurrentCharacter)
        {
            _balk.CurrentCharacter.SetStartDragPosition();
            _startDragPosition = transform.position;
        }
    }

    public void DragBalk(Vector3 currentPosition)
    {
        if (_balk.CurrentCharacter)
        {
            Vector3 offset = currentPosition - _startDragPosition;

            transform.position = _startDragPosition + Vector3.ClampMagnitude(offset, _maxDragDistance);
        }
    }

    public void FinishDragBalk()
    {
        if (_balk.CurrentCharacter)
        {
            Vector3 endDragPosition = transform.position;
            Vector3 direction = _startDragPosition - endDragPosition;

            Debug.Log(direction.normalized);

            _balk.CurrentCharacter.Push(direction.normalized);
            _balk.CurrentCharacter.DetachFromBalk();

            //StartCoroutine(DisableColliderOnTime(_timeToLeaveBalk));  //NEED TEST
        }
    }

    private IEnumerator DisableColliderOnTime(float delay)
    {
        _balkCollider.enabled = false;

        yield return new WaitForSeconds(delay);

        _balkCollider.enabled = true;
    }
}
