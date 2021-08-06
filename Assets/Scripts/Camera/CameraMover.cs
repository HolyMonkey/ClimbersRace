using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _lookAtOffset;
    [SerializeField] private float _offsetDistance;
    [SerializeField] private MonoBehaviour _wallBehavior;
    private IWall _wall => (IWall)_wallBehavior;

    private Camera _camera;

    private void OnValidate()
    {
        if (_wallBehavior is IWall || !_wallBehavior)
            return;

        Debug.LogError(name + " needs to implement " + nameof(IWall));
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _wall.GetNormalVector(_character.transform.position);
        targetPosition.x *= _offsetDistance;
        targetPosition.z *= _offsetDistance;
        targetPosition.y = _character.transform.position.y;

        targetPosition += _positionOffset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        transform.LookAt(_character.transform.position + _lookAtOffset);
    }

    public void UpdateFOV(float multiplierFOV)
    {

    }
}
