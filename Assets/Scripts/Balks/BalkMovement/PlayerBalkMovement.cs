using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalkMovement : MonoBehaviour
{
    [SerializeField] private BalkMovementHandler _balkMovement;

    private Vector3 _offset;
    private float _zOffset;

    private void Start()
    {
        _zOffset = Camera.main.WorldToScreenPoint(transform.localPosition).z;
    }

    private void OnMouseDown()
    {
        _offset = transform.position - GetMousePosition();
        _balkMovement.BeginDragBalk();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMousePosition() + _offset;

        _balkMovement.DragBalk(newPosition);
    }

    private void OnMouseUp()
    {
        _balkMovement.FinishDragBalk();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zOffset;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
