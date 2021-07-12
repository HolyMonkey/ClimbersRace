using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(SpringJoint))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _temporaryRigidBody;
    [SerializeField] private float _springTougthness = 5000f;
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 30f;
    [SerializeField, Range(0f, 100f)] private float _swingReducerPower = 10f;
    [SerializeField, Range(0f, 1f)] private float _minDragForceToJump = 0.06f;
    [SerializeField, Range(0f, 1f)] private float _timeToLeaveHook = 0.05f;
    [SerializeField, Range(0f, 100f)] private float _maxAttachingSpeed = 10f;

    private Vector3 _startDragPosition;
    private bool _attachedToHook;
    private Collider _collider;
    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;

    public float MaxAttachingSpeed => _maxAttachingSpeed;
    public Rigidbody Rigidbody => _rigidbody;

    public event UnityAction CatchedBalkOnRight;
    public event UnityAction CatchedBalkOnLeft;
    public event UnityAction LeftBalk;
    public event UnityAction SlidingDown;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _springJoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        ClampMaxSpeed();

        _attachedToHook = _springJoint.connectedBody != _temporaryRigidBody;

        if (_rigidbody.velocity.magnitude > _maxAttachingSpeed)
        {
            StartCoroutine(SwingHook(0.5f));
        }
    }

    public void ClampMaxSpeed()
    {
        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }
    }

    public void StartDrag()
    {
        if (_attachedToHook)
        {
            _startDragPosition = transform.localPosition;
        }
    }

    public void AttachToHook(Balk hook)
    {
        _rigidbody.drag = _swingReducerPower;
        _springJoint.connectedBody = hook.Rigidbody;
        _springJoint.connectedAnchor = _springJoint.connectedBody.centerOfMass;
        _springJoint.spring = _springTougthness;

        SetLookDirection(0f);
    }

    public void DetachFromHook()
    {
        if (_attachedToHook && GetDragPowerLetToFly())
        {
            StartCoroutine(LetFlyThrough(_timeToLeaveHook));
            ResetJoint();
            LeftBalk?.Invoke();
        }
    }

    public void SetLookDirection(float rotationOffset)
    {
        if (_startDragPosition.x - transform.localPosition.x <= rotationOffset)
        {
            CatchedBalkOnLeft?.Invoke();
        }
        else if (_startDragPosition.x - transform.localPosition.x > rotationOffset)
        {
            CatchedBalkOnRight?.Invoke();
        }
    }

    public IEnumerator SwingHook(float delay)
    {
        SlidingDown?.Invoke();
        _collider.enabled = false;

        yield return new WaitForSeconds(delay);

        ResetJoint();
        _collider.enabled = true;
    }

    private bool GetDragPowerLetToFly()
    {
        return Vector3.Distance(_startDragPosition, transform.localPosition) > _minDragForceToJump;
    }

    private void ResetJoint()
    {
        _rigidbody.drag = default;
        _springJoint.connectedBody = _temporaryRigidBody;
        _springJoint.connectedAnchor = _springJoint.anchor;
        _springJoint.spring = 0f;
    }

    private IEnumerator LetFlyThrough(float delay)
    {
        _collider.enabled = false;

        yield return new WaitForSeconds(delay);

        _collider.enabled = true;
    }
}

