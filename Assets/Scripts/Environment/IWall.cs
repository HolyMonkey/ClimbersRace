using UnityEngine;

public interface IWall
{
    public Vector3 GetTangentXZVector(Vector3 position);
    public Vector3 GetNormalVector(Vector3 position);
}