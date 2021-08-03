using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylindricMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _baseSpeed;
    [SerializeField] private CylindricWall _cylindricWall;

    private Rigidbody _rigidbody;

    private Vector3 _direction;
    private float _force;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 onNormal = _cylindricWall.GetTangentVector(_rigidbody.position);

        float targetMagnitude = _rigidbody.velocity.magnitude;

        Vector3 projectVelocity = Vector3.Project(_rigidbody.velocity, onNormal);
        projectVelocity.y = _rigidbody.velocity.y;

        projectVelocity = projectVelocity.normalized * targetMagnitude;

        _rigidbody.velocity = projectVelocity;
    }

    public void Move(Vector3 direction, float force)
    {
        _direction = direction;
        _force = force;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(_direction * _force, ForceMode.Impulse);
    }
}