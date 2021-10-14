using UnityEngine;

public interface IMovable
{
    public void Move(Vector3 direction, float force);
    public void UpdateVelocity();
}