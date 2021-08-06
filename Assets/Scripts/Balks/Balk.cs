using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Balk : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _nearPoint;
    [SerializeField] private Transform _farPoint;

    public Rigidbody Rigidbody => _rigidbody;
    public Transform NearPoint => _nearPoint;
    public Transform FarPoint => _farPoint;

    public Character CurrentCharacter;

    private void Start()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}
