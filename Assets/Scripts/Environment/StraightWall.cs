using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightWall : MonoBehaviour, IWall
{
    private Vector3 _normalPoint = new Vector3();

    public Vector3 GetNormalVector(Vector3 position)
    {
        _normalPoint.x = position.x;
        _normalPoint.y = position.y;
        _normalPoint.z = transform.position.z;

        Debug.DrawLine(_normalPoint, position, Color.black);
        return (position - _normalPoint).normalized;
    }

    public Vector3 GetTangentXZVector(Vector3 position)
    {
        throw new System.NotImplementedException();
    }
}
