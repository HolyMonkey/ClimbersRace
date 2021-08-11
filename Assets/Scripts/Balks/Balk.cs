using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Balk : MonoBehaviour
{
    [SerializeField] private float _forceOnAttach;
    [SerializeField] private Rigidbody _jointRigidbody;
    [SerializeField] private Transform _nearPoint;
    [SerializeField] private Transform _farPoint;
    [SerializeField] private Transform _lookAtPoint;

    private Rigidbody _rigidbody;
    private Character _currentCharacter;

    public bool HasCharacter => _currentCharacter;
    public Rigidbody JointRigidbody => _jointRigidbody;
    public Transform NearPoint => _nearPoint;
    public Transform FarPoint => _farPoint;
    public Vector3 LookAtPoint => _lookAtPoint.position;

    public Vector3 PushVector = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PushCharacter(Vector3 pushVector)
    {
        _currentCharacter.BalkPush(pushVector);

        _currentCharacter = null;

        PushVector = Vector3.zero;
    }

    public void AddForce(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _forceOnAttach, ForceMode.Impulse);
    }

    public virtual void Interaction(Character character)
    {
        _currentCharacter = character;
    }
}
