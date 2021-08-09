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

    public Rigidbody JointRigidbody => _jointRigidbody;
    public Transform NearPoint => _nearPoint;
    public Transform FarPoint => _farPoint;
    public Vector3 LookAtPoint => _lookAtPoint.position;

    public Character CurrentCharacter;
    public Vector3 PushVector = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PushCharacter()
    {
        CurrentCharacter.Push(PushVector.normalized);

        CurrentCharacter = null;

        PushVector = Vector3.zero;
    }

    public void AddForce(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _forceOnAttach, ForceMode.Impulse);
    }
}
