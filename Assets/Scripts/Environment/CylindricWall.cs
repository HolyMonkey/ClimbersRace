using UnityEngine;

public class CylindricWall : MonoBehaviour, IWall
{
    private Vector3 _centerPoint;

    private void Awake()
    {
        _centerPoint = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public Vector3 GetTangentXZVector(Vector3 position)
    {
        Vector3 normalVector = GetNormalVector(position);
        return new Vector3(-normalVector.z, 0, normalVector.x);
    }

    public Vector3 GetNormalVector(Vector3 position)
    {
        _centerPoint.y = position.y;
        return (position - _centerPoint).normalized;
    }
}
