using UnityEngine;

public class StraightMovement : MonoBehaviour, IMovable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void UpdateVelocity()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, 0f);
    }

    public void Move(Vector3 direction, float force)
    {
        _rigidbody.velocity = Vector3.zero;
        direction.z = 0;
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
}
