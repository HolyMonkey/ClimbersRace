using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Balk : MonoBehaviour
{
    [SerializeField] private Rigidbody _jointRigidbody;
    [SerializeField] private Transform _nearPoint;
    [SerializeField] private Transform _farPoint;
    [SerializeField] private Transform _lookAtPoint;

    public Rigidbody Rigidbody => _jointRigidbody;
    public Transform NearPoint => _nearPoint;
    public Transform FarPoint => _farPoint;
    public Vector3 LookAtPoint => _lookAtPoint.position;

    public Character CurrentCharacter;
    public Vector3 PushVector = Vector3.zero;

    private void Start()
    {
        if (_jointRigidbody == null)
        {
            _jointRigidbody = GetComponent<Rigidbody>();
        }
    }

    public void PushCharacter()
    {
        CurrentCharacter.Push(PushVector.normalized);

        CurrentCharacter = null;

        PushVector = Vector3.zero;
    }
}
