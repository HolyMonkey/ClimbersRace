using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _baseSpeed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction, float force)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
}
