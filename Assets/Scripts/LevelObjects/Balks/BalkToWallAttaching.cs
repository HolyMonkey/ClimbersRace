using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkToWallAttaching : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _wallBehavior;
    private IWall _wall => (IWall)_wallBehavior;

    private void OnValidate()
    {
        if (!(_wallBehavior is IWall) && _wallBehavior)
            Debug.LogError(name + " needs to implement " + nameof(IWall));
    }

    [ContextMenu("RotateFromWall")]
    private void RotateFromWall()
    {
        Vector3 direction = -_wall.GetNormalVector(transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }
}