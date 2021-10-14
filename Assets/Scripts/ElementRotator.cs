using UnityEngine;

public class ElementRotator : MonoBehaviour
{
    [SerializeField] private float _speedRotate;
    [SerializeField] private Vector3 _rotateDirection;

    private Vector3 _currentAngel;
    private Vector3 _startAngel;

    private void Awake()
    {
        _startAngel = transform.eulerAngles;
    }

    private void OnEnable()
    {
        _currentAngel = _startAngel;
    }

    private void Update()
    {
        _currentAngel += _rotateDirection * _speedRotate * Time.deltaTime;
        transform.eulerAngles = _currentAngel;
    }
}
