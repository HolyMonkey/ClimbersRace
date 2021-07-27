using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(SpringJoint))]
public class CharacterInteractionHandler : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _springTougthness;
    [SerializeField] private float _swingReducerPower;
    [SerializeField] private float _timeToLeaveBalk;
    [SerializeField] private Rigidbody _defaultRigidbody;

    private Vector3 _startDragPosition;
    private bool _isAttachingBalk;
    private float _distanceToBalk;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private SpringJoint _springJoint;

    public event UnityAction<Balk> AttachingBalk;
    public event UnityAction DetachingBalk;
    public event UnityAction SlidingDown;

    public bool IsAttachingBalk => _isAttachingBalk;
    public float DistanceToBalk => _distanceToBalk;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
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

    public void Push(Vector2 direction)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * _pushForce, ForceMode.Impulse);
    }

    public void PushInDirectionMovement(float force)
    {
        Push(_rigidbody.velocity.normalized * force);
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
        StartCoroutine(DisableColliderOnTime(_timeToLeaveBalk));
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

    private IEnumerator DisableColliderOnTime(float delay)
    {
        _collider.enabled = false;

        yield return new WaitForSeconds(delay);

        _collider.enabled = true;
    }
}
