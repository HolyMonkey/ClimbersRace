using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(SpringJoint))]
public class MovementHandler : MonoBehaviour
{
    [SerializeField] private HoldBalkIK _keepOnIK;
    [SerializeField] private Rigidbody _tempRigidbody;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _springTougthness;
    [SerializeField] private float _swingReducerPower;
    [SerializeField] private float _timeToLeaveBalk;

    private Collider _collider;
    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;
    private Vector3 _startDragPosition;
    private bool _catchedOnBalk;

    public event UnityAction CatchedBalkOnRight;
    public event UnityAction CatchedBalkOnLeft;
    public event UnityAction SlidingDown;

    public bool CatchedOnBalk => _catchedOnBalk;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _springJoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }

    public void Push(Vector2 direction)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * _pushForce, ForceMode.Impulse);
    }

    public void PushInDirectionMovement(float force)
    {
        Push(_rigidbody.velocity.normalized * force);
    }

    public void AttachToBalk(Balk balk)
    {
        _catchedOnBalk = true;
        _rigidbody.drag = _swingReducerPower;
        _springJoint.connectedBody = balk.Rigidbody;
        _springJoint.connectedAnchor = balk.Rigidbody.centerOfMass;
        _springJoint.spring = _springTougthness;

        SetHandGripDirection(balk);
    }

    public void DetachFromBalk()
    {
        _catchedOnBalk = false;
        ResetJoint();
        _keepOnIK.SetTarget(null, null);
        StartCoroutine(LetFlyThrough(_timeToLeaveBalk));
    }

    public void SetStartDragPosition(Vector3 startDragPosition)
    {
        _startDragPosition = startDragPosition;
    }

    public void CollideWithTrap()
    {
        SlidingDown?.Invoke();
        DetachFromBalk();
    }

    public void FallDown()
    {
        _collider.enabled = false;
        ResetJoint();
    }

    private void ResetJoint()
    {
        _rigidbody.drag = 0;
        _springJoint.connectedBody = _tempRigidbody;
        _springJoint.connectedAnchor = _springJoint.anchor;
        _springJoint.spring = 0f;
    }

    private void SetHandGripDirection(Balk balk)
    {
        float distanceToBalk = _startDragPosition.x - transform.position.x;

        if (distanceToBalk > 0.05f)
        {
            _keepOnIK.SetTarget(balk.NearPoint, balk.FarPoint);
            CatchedBalkOnLeft?.Invoke();
        }
        else
        {
            _keepOnIK.SetTarget(balk.FarPoint, balk.NearPoint);
            CatchedBalkOnRight?.Invoke();
        }
    }

    private IEnumerator LetFlyThrough(float delay)
    {
        _collider.enabled = false;

        yield return new WaitForSeconds(delay);

        _collider.enabled = true;
    }
}
