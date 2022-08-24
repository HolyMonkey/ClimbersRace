using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void OnMouseDrag()
    {
        float axisRotationX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * -axisRotationX * _speed * Time.deltaTime);
    }
}
