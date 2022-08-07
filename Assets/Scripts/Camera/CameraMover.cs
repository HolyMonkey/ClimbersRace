using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private BonusWall _startPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _lookAtOffset;
<<<<<<< HEAD
    [SerializeField] private Vector3 _positionOffsetOnWon;
=======
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
    [SerializeField] private Vector3 _lookAtOffsetOnWon;
    [SerializeField] private float _distanceOffset;
    [SerializeField] private MonoBehaviour _wallBehavior;
    [SerializeField] private Level _level;
    [SerializeField] private float _duration;

    [Header("DragImpact")]
    [SerializeField] private RangeFloat _minMaxFOV;
    [SerializeField] private float _scaleInTime = 1f;
    [SerializeField] private float _scaleOutTime = 1.5f;
    [SerializeField] private Ease _ease;

    private IWall _wall => (IWall)_wallBehavior;
    private Tweener _dragImpactTweener;
    private bool _isFOVScaled = false;
    private bool _isTargetReached = false;
    private Coroutine _moveCameraInJob;

    private Camera _camera;

    public bool IsStartReached => _isTargetReached;

    private void OnValidate()
    {
        if (_wallBehavior is IWall || !_wallBehavior)
            return;

        Debug.LogError(name + " needs to implement " + nameof(IWall));
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.transform.position = _startPoint.transform.position + new Vector3(0, 0, -13.75f);
    }

    private void OnEnable()
    {
        _level.LevelPreStart += OnChangePosition;
        _level.LevelWon += OnLevelWon;

    }

    private void OnDisable()
    {
        _level.LevelPreStart -= OnChangePosition;
        _level.LevelWon -= OnLevelWon;
    }

    private void FixedUpdate()
    {
        if (_isTargetReached)
        {
            Follow();
            transform.LookAt(_target.position + _lookAtOffset);
        }
    }

    public void OnLevelWon()
    {
        _lookAtOffset = _lookAtOffsetOnWon;
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

    public void ChangeTarget(Character character)
    {
        _target = character.transform;
        _smoothSpeed = 2f;
    }

    public void OnLevelWon()
    {
        _positionOffset = _positionOffsetOnWon;
        _lookAtOffset = _lookAtOffsetOnWon;
    }

    private void Follow()
    {
        Vector3 targetPosition = _wall.GetNormalVector(_target.position);

        if (_wall is StraightWall)
            targetPosition.x = _target.position.x;
        else
            targetPosition.x *= _distanceOffset;

        targetPosition.z *= _distanceOffset;
        targetPosition.y = _target.position.y;

        targetPosition += _positionOffset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
    }

    private IEnumerator MoveCamera(Transform startPoint, Transform targetPoint)
    {
        _camera.transform.DOMove(targetPoint.position - new Vector3(0, 0, 8.8f), _duration);
        yield return new WaitForSeconds(_duration);
        _isTargetReached = true;
    }

    private void StopChangePosition()
    {
        StopCoroutine(_moveCameraInJob);
    }

    private void OnChangePosition()
    {
        if (_moveCameraInJob != null)
            StopChangePosition();

        _moveCameraInJob = StartCoroutine(MoveCamera(_startPoint.transform, _target));
    }

}
