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

    public Balk CurrentBalk { get; private set; }
    private Vector3 _startDragPosition;

    public event UnityAction<Balk> AttachingBalk;
    public event UnityAction DetachingBalk;
    public event UnityAction Falling;

    public bool IsAttachingBalk => CurrentBalk;
    public Vector3 PushVector => CurrentBalk.PushVector;
    public Vector3 Velocity => _rigidbody.velocity;

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
    }

    public void Push(Vector3 direction)
    {
        DetachFromBalk();
        _mover.Move(direction, _pushForce);
    }

    public void AttachToBalk(Balk balk)
    {
        balk.CurrentCharacter = this;
        balk.AddForce(Velocity);
        CurrentBalk = balk;

        SetupJoint(_swingReducerPower, balk.JointRigidbody, balk.JointRigidbody.centerOfMass, _springTougthness);


        AttachingBalk?.Invoke(balk);
    }

    public void DetachFromBalk()
    {
        if (CurrentBalk)
        {
            CurrentBalk = null;
        }

        SetupJoint(0, _defaultRigidbody, _springJoint.anchor, 0);

        DetachingBalk?.Invoke();
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
