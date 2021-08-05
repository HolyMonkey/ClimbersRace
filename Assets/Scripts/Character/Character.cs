using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(SpringJoint))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _springTougthness;
    [SerializeField] private float _swingReducerPower;
    [SerializeField] private Rigidbody _defaultRigidbody;
    [SerializeField]
    private MonoBehaviour _moverBehaviour;
    private IMovable _mover => (IMovable)_moverBehaviour;

    private Vector3 _startDragPosition;
    private bool _isAttachingBalk;
    private float _distanceToBalk;
    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;

    public event UnityAction<Balk> AttachingBalk;
    public event UnityAction DetachingBalk;
    public event UnityAction SlidingDown;

    public bool IsAttachingBalk => _isAttachingBalk;
    public float DistanceToBalk => _distanceToBalk;

    private void OnValidate()
    {
        if (_moverBehaviour is StraightMovement)
            GetComponent<CylindricMovement>().enabled = false;
        else
            GetComponent<CylindricMovement>().enabled = true;

        if (_moverBehaviour is IMovable)
            return;

        Debug.LogError(_moverBehaviour.name + " needs to implement " + nameof(IMovable));
        _moverBehaviour = null;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _springJoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);

        if (_isAttachingBalk)
        {
            _distanceToBalk = _startDragPosition.x - transform.localPosition.x;
        }
    }

    public void Push(Vector3 direction)
    {
        _mover.Move(direction, _pushForce);
    }

    public void SetStartDragPosition()
    {
        _startDragPosition = transform.localPosition;
    }

    public void AttachToBalk(Balk balk)
    {
        AttachingBalk?.Invoke(balk);

        _isAttachingBalk = true;
        SetupJoint(_swingReducerPower, balk.Rigidbody, balk.Rigidbody.centerOfMass, _springTougthness);
    }

    public void DetachFromBalk()
    {
        DetachingBalk?.Invoke();

        _isAttachingBalk = false;
        SetupJoint(0, _defaultRigidbody, _springJoint.anchor, 0);
    }

    public void CollideWithTrap()
    {
        SlidingDown?.Invoke();
        DetachFromBalk();
    }

    private void SetupJoint(float drag, Rigidbody connectedBody, Vector3 connectedAnchor, float spring)
    {
        _rigidbody.drag = drag;
        _springJoint.connectedBody = connectedBody;
        _springJoint.connectedAnchor = connectedAnchor;
        _springJoint.spring = spring;
    }
}
