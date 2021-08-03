using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylindricWall : MonoBehaviour
{
    public Vector3 CenterAxis { get; private set; }
    private float _radius;
    private void Awake()
    {
        CenterAxis = new Vector3(0, 0, 2.2f);
        _radius = transform.localScale.x / 2;
    }

    public Vector3 GetTangentVector(Vector3 position)
    {
        Vector3 normalVector = position - CenterAxis;
        return new Vector3(-normalVector.z, 0, normalVector.x);
    }
}
