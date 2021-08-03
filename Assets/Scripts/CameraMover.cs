using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private CharacterInteraction _character;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _offsetDistance;
    [SerializeField] private MonoBehaviour _wallBehavior;
    private IWall _wall => (IWall)_wallBehavior;

    private Camera _camera;
    private Vector3 _lookAtOffset = Vector3.zero;

    private void OnValidate()
    {
        if (_wallBehavior is IWall)
            return;

        Debug.LogError(_wallBehavior.name + " needs to implement " + nameof(IWall));
        _wallBehavior = null;
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        transform.LookAt(_character.transform.position + _lookAtOffset);
    }

    public void UpdateFOV(float multiplierFOV)
    {

    }
}
