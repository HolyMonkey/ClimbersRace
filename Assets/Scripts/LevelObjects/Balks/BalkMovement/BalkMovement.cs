using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkMovement : MonoBehaviour
{
    [SerializeField] private RangeFloat _minMaxDragDistance;

    private Balk _balk;
    private Vector3 _startDragPosition;

    private void Awake()
    {
        _balk = GetComponent<Balk>();

        _startDragPosition = transform.position;
    }

    public void BeginDragBalk()
    {
        if (_balk.HasCharacter)
        {
            _startDragPosition = transform.position;
        }
    }

    public void DragBalk(Vector3 targetPosition)
    {
        if (_balk.HasCharacter)
        {
            Vector3 dragVector = Vector3.ClampMagnitude(targetPosition - _startDragPosition, _minMaxDragDistance.Max);

            DragingBalk(dragVector);
        }
    }

    public void DragBalk(Vector3 direction, float dragValue)
    {
        if (_balk.HasCharacter)
        {
            dragValue = Mathf.Lerp(_minMaxDragDistance.Min, _minMaxDragDistance.Max, dragValue);
            Vector3 dragVector = direction * dragValue;

            DragingBalk(dragVector);
        }
    }

    public void FinishDragBalk()
    {
        if (_balk.HasCharacter)
        {
            _balk.ScaleCamera(0);

            if (_balk.PushVector.magnitude >= _minMaxDragDistance.Min)
                _balk.PushCharacter(_balk.PushVector.normalized);
        }
    }

    private void DragingBalk(Vector3 dragVector)
    {
        float dragT = dragVector.magnitude / _minMaxDragDistance.Max;
        _balk.ScaleCamera(dragT);

        transform.position = _startDragPosition + dragVector;

        _balk.PushVector = -dragVector;
    }
}
