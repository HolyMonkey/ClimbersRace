using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BalkMover : MonoBehaviour
{
    private Vector3 _offset = Vector3.zero;
    private PlayerMover _playerMover;

    private float _zOffset;

    private void Start()
    {
        _zOffset = Camera.main.WorldToScreenPoint(transform.localPosition).z;
    }

    private void OnMouseDown()
    {
        _playerMover.Movement.StartDrag();
        _offset = transform.position - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = GetMousePosition();
        transform.position = mousePosition + _offset;
    }

    private void OnMouseUp()
    {
        StartCoroutine(SetDetatchDelay(0.06f));
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zOffset;

        return Camera.main.ScreenToViewportPoint(mousePosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            _playerMover = playerMover;
        }
    }

    private IEnumerator SetDetatchDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _playerMover.Movement.DetatchFromHook();
    }
}
