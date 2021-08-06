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

    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;

    private Balk _currentBalk;
    private Vector3 _startDragPosition;

    public event UnityAction<Balk> AttachingBalk;
    public event UnityAction DetachingBalk;
    public event UnityAction Falling;

    public bool IsAttachingBalk => _currentBalk ? true : false;
    public Vector3 PushVector { get; private set; }

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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _springJoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);

        if (IsAttachingBalk)
        {
            PushVector = transform.localPosition - _startDragPosition;
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

        balk.CurrentCharacter = this;
        _currentBalk = balk;

        SetupJoint(_swingReducerPower, balk.Rigidbody, balk.Rigidbody.centerOfMass, _springTougthness);
    }

    public void DetachFromBalk()
    {
        DetachingBalk?.Invoke();

        if (_currentBalk)
        {
            _currentBalk.CurrentCharacter = null;
            _currentBalk = null;
        }

        SetupJoint(0, _defaultRigidbody, _springJoint.anchor, 0);
    }

    public void CollideWithTrap()
    {
        Falling?.Invoke();
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
