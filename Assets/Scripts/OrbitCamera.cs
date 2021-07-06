using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform focus = default;
    [SerializeField, Range(1f, 20f)] private float _distance = 5f;
    [SerializeField, Min(0f)] private float _focusRadius = 5f;
    [SerializeField, Range(0f, 1f)] float focusCentering = 0.5f;
    [SerializeField, Range(-89f, 89f)] float minVerticalAngle = -45f, maxVerticalAngle = 45f;

    Vector3 focusPoint;
    Vector2 orbitAngles = new Vector2(15f, -6f);

    void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

    void Awake()
    {
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);
    }

    void LateUpdate()
    {
        UpdateFocusPoint();
        Quaternion lookRotation;

        lookRotation = transform.localRotation;

        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * _distance;

        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    void UpdateFocusPoint()
    {
        Vector3 targetPoint = focus.position;
        if (_focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            if (distance > 0.01f && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }
            if (distance > _focusRadius)
            {
                t = Mathf.Min(t, _focusRadius / distance);
            }
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            focusPoint = targetPoint;
        }
    }
}

