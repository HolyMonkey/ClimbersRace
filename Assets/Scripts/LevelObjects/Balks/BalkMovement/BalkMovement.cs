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
            float dragDistance = Mathf.Lerp(_minMaxDragDistance.Min, _minMaxDragDistance.Max, dragValue);
            Vector3 dragVector = direction * dragDistance;

            DragingBalk(dragVector);
        }
    }

    public float PlayerDragBalk(Vector3 targetPosition)
    {
        if (_balk.HasCharacter)
        {
            Vector3 dragVector = Vector3.ClampMagnitude(targetPosition - _startDragPosition, _minMaxDragDistance.Max);

            DragingBalk(dragVector);

            return dragVector.magnitude / _minMaxDragDistance.Max;
        }
        else
            return 0f;
    }

    public void FinishDragBalk()
    {
        if (_balk.HasCharacter)
        {
            if (_balk.PushVector.magnitude >= _minMaxDragDistance.Min)
                _balk.PushCharacter(_balk.PushVector.normalized);
        }
    }

    private void DragingBalk(Vector3 dragVector)
    {
        transform.position = _startDragPosition + dragVector;

        _balk.PushVector = -dragVector;
    }
}
