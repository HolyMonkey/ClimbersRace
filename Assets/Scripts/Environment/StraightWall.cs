using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightWall : MonoBehaviour, IWall
{
    private Vector3 _normalPoint;

    public Vector3 GetNormalVector(Vector3 position)
    {
        _normalPoint = position;

        _normalPoint.z = transform.position.z;
        _normalPoint.x = position.x;
        _normalPoint.y = position.y;

        Debug.DrawLine(_normalPoint, position, Color.black);
        return (position - _normalPoint).normalized;
    }

    public Vector3 GetTangentXZVector(Vector3 position)
    {
        throw new System.NotImplementedException();
    }
}
