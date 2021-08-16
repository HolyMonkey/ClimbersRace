using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightWall : MonoBehaviour, IWall
{
    private Vector3 _normalPoint = Vector3.back;

    public Vector3 GetNormalVector(Vector3 position)
    {
        //_normalPoint.x = position.x;
        //_normalPoint.y = position.y;
        //_normalPoint.z = transform.position.z;

        return _normalPoint;
    }

    public Vector3 GetTangentXZVector(Vector3 position)
    {
        throw new System.NotImplementedException();
    }
}
