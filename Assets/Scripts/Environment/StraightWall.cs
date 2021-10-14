using UnityEngine;

public class StraightWall : MonoBehaviour, IWall
{
    private Vector3 _normalPoint = Vector3.back;

    public Vector3 GetNormalVector(Vector3 position)
    {
        return _normalPoint;
    }

    public Vector3 GetTangentXZVector(Vector3 position)
    {
        throw new System.NotImplementedException();
    }
}
