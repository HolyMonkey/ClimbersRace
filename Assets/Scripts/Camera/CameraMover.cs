using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _lookAtOffset;
    [SerializeField] private float _distanceOffset;
    [SerializeField] private MonoBehaviour _wallBehavior;
    private IWall _wall => (IWall)_wallBehavior;

    [Header("DragImpact")]
    [SerializeField] private RangeFloat _minMaxFOV;
    [SerializeField] private float _scaleInTime = 1f;
    [SerializeField] private float _scaleOutTime = 1.5f;
    [SerializeField] private Ease _ease;

    private Tweener _dragImpactTweener;
    private bool _isFOVScaled = false;

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

        if (_wall is StraightWall)
            targetPosition.x = _character.transform.position.x;
        else
            targetPosition.x *= _distanceOffset;

        targetPosition.z *= _distanceOffset;
        targetPosition.y = _character.transform.position.y;

        targetPosition += _positionOffset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        transform.LookAt(_character.transform.position + _lookAtOffset);
    }

    public void ScaleFOV(float balkDragValue)
    {
        if (balkDragValue > 0.9f)
        {
            if (!_isFOVScaled)
            {
                _dragImpactTweener.Kill();
                _dragImpactTweener = _camera.DOFieldOfView(_minMaxFOV.Max, _scaleInTime).SetEase(_ease);
                _isFOVScaled = true;
            }
        }
        else
        {
            if (_isFOVScaled)
            {
                _dragImpactTweener.Kill();
                _dragImpactTweener = _camera.DOFieldOfView(_minMaxFOV.Min, _scaleOutTime).SetEase(_ease);
                _isFOVScaled = false;
            }
        }
    }
}
