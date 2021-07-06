using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(SpringJoint))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _temporaryRigidBody = default;
    [SerializeField] private float _springTougthness = 5000f;
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 30f;
    [SerializeField, Range(0f, 100f)] private float _swingReducerPower = 10f;
    [SerializeField, Range(0f, 1f)] private float _minDragForceToJump = 0.06f;
    [SerializeField, Range(0f, 1f)] private float _timeToLeaveHook = 0.1f;
    [SerializeField, Range(0f, 100f)] private float _maxAttachingSpeed = 10f;

    private Vector3 _startPosition = Vector3.zero;

    private Collider _collider;
    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;

    public float MaxAttachingSpeed => _maxAttachingSpeed;
    public Rigidbody Rigidbody => _rigidbody;
    public Vector3 StartPosition => _startPosition;

    public event UnityAction CatchedtBalkOnRight;
    public event UnityAction CatchedtBalkOnLeft;
    public event UnityAction LeftBalk;
    public event UnityAction SlidDown;

    public bool AttachedToHook { get; private set; }

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _springJoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        AttachedToHook = _springJoint.connectedBody != _temporaryRigidBody;
    }

    public void ClampMexSpeed()
    {
        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }
    }

    public void StartDrag()
    {
        if (AttachedToHook)
        {
            _startPosition = transform.localPosition;
        }
    }

    public IEnumerator SwingHook(float delay)
    {
        SlidDown?.Invoke();
        yield return new WaitForSeconds(delay);
        ResetJoint();
        _collider.enabled = false;
    }

    public void AttachToHook(Balk hook)
    {
        _rigidbody.drag = _swingReducerPower;
        _springJoint.connectedBody = hook.Rigidbody;
        _springJoint.connectedAnchor = _springJoint.connectedBody.centerOfMass;
        _springJoint.spring = _springTougthness;

        SetLookDirection(0f);
    }

    public void DetatchFromHook()
    {
        if (AttachedToHook && GetDragPowerLetToFly())
        {
            StartCoroutine(LetFlyThrough(_timeToLeaveHook));
            ResetJoint();
            LeftBalk?.Invoke();
        }
    }

    private bool GetDragPowerLetToFly()
    {
        return Vector3.Distance(_startPosition, transform.localPosition) > _minDragForceToJump;
    }

    private IEnumerator LetFlyThrough(float delay)
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(delay);
        _collider.enabled = true;
    }

    private void ResetJoint()
    {
        _rigidbody.drag = default;
        _springJoint.connectedBody = _temporaryRigidBody;
        _springJoint.connectedAnchor = _springJoint.anchor;
        _springJoint.spring = 0f;
    }

    public void SetFlyDirection(float rotationOffset)
    {
        if (StartPosition.x - transform.localPosition.x <= rotationOffset)
        {
            CatchedtBalkOnRight?.Invoke();
        }
        else if (StartPosition.x - transform.localPosition.x > rotationOffset)
        {
            CatchedtBalkOnLeft?.Invoke();
        }
    }

    public void SetLookDirection(float rotationOffset)
    {
        if (StartPosition.x - transform.localPosition.x <= rotationOffset)
        {
            CatchedtBalkOnLeft?.Invoke();
        }
        else if (StartPosition.x - transform.localPosition.x > rotationOffset)
        {
            CatchedtBalkOnRight?.Invoke();
        }
    }
}

