using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _focus;
    [SerializeField] private Vector3 _offset;
    [SerializeField, Min(0f)] private float _focusRadius = 5f;
    [SerializeField, Range(0f, 1f)] private float _focusCentering = 0.5f;
    [SerializeField, Range(-89f, 89f)] private float _minVerticalAngle = -45f, _maxVerticalAngle = 45f;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private Vector3 _focusPoint;
    private Vector2 _orbitAngles = new Vector2(15f, -6f);

    private void OnValidate()
    {
        if (_maxVerticalAngle < _minVerticalAngle)
        {
            _maxVerticalAngle = _minVerticalAngle;
        }
    }

    private void Awake()
    {
        _focusPoint = _focus.position;
        transform.localRotation = Quaternion.Euler(_orbitAngles);
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();
        Vector3 lookPosition = _focusPoint + _offset;
        lookPosition.x = Mathf.Clamp(lookPosition.x, _minX, _maxX);

        transform.position = lookPosition;
    }

    public void SetTarget(Transform target)
    {
        _focus = target;
    }

    private void UpdateFocusPoint()
    {
        Vector3 targetPoint = _focus.position;
        if (_focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, _focusPoint);
            float time = 1f;
            if (distance > 0.01f && _focusCentering > 0f)
            {
                time = Mathf.Pow(1f - _focusCentering, Time.unscaledDeltaTime);
            }
            if (distance > _focusRadius)
            {
                time = Mathf.Min(time, _focusRadius / distance);
            }
            _focusPoint = Vector3.Lerp(targetPoint, _focusPoint, time);
        }
        else
        {
            _focusPoint = targetPoint;
        }
    }
}

