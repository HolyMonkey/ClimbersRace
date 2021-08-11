using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkInteractionHandler : MonoBehaviour
{
    [SerializeField] private Collider _balkCollider;
    [SerializeField] private RangeFloat _minMaxDragDistance;
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
            _startDragPosition = transform.position;
        }
    }

    public void DragBalk(Vector3 targetPosition)
    {
        if (_balk.CurrentCharacter)
        {
            Vector3 dragVector = Vector3.ClampMagnitude(targetPosition - _startDragPosition, _minMaxDragDistance.Max);

            transform.position = _startDragPosition + dragVector;

            _balk.PushVector = -dragVector;
        }
    }

    public void FinishDragBalk()
    {
        if (_balk.CurrentCharacter && _balk.PushVector.magnitude >= _minMaxDragDistance.Min)
        {
            _balk.PushCharacter();

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
