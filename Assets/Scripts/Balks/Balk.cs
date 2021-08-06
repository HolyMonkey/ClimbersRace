using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Balk : MonoBehaviour
{
    [SerializeField] private Rigidbody _jointRigidbody;
    [SerializeField] private Transform _nearPoint;
    [SerializeField] private Transform _farPoint;

    public Rigidbody Rigidbody => _jointRigidbody;
    public Transform NearPoint => _nearPoint;
    public Transform FarPoint => _farPoint;

    public Character CurrentCharacter;

    private void Start()
    {
        if (_jointRigidbody == null)
        {
            _jointRigidbody = GetComponent<Rigidbody>();
        }
    }
}
