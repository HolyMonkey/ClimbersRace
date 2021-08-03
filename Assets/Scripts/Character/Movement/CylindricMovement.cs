using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylindricMovement : MonoBehaviour, IMovable
{
    [SerializeField] private CylindricWall _cylindricWall;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 cylinderTangent = _cylindricWall.GetTangentXZVector(_rigidbody.position);

        float targetMagnitude = _rigidbody.velocity.magnitude;

        Vector3 projectVelocity = Vector3.Project(_rigidbody.velocity, cylinderTangent);
        projectVelocity.y = _rigidbody.velocity.y;

        projectVelocity = projectVelocity.normalized * targetMagnitude;

        _rigidbody.velocity = projectVelocity;

    }

    public void Move(Vector3 direction, float force)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
}