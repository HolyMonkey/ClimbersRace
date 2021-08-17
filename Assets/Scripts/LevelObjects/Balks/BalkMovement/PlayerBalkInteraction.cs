using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BalkMovement))]
public class PlayerBalkInteraction : MonoBehaviour
{
    private BalkMovement _balkMovement;

    private Camera _camera;

    private Vector3 _mouseOffset;
    private float _zMouseOffset;

    private void Awake()
    {
        _camera = Camera.main;
        _balkMovement = GetComponent<BalkMovement>();
    }

    private void OnMouseDown()
    {
        _zMouseOffset = _camera.WorldToScreenPoint(transform.position).z;
        _mouseOffset = transform.position - GetMousePosition();
        _balkMovement.BeginDragBalk();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMousePosition() + _mouseOffset;
        _balkMovement.DragBalk(newPosition);
    }

    private void OnMouseUp()
    {
        _balkMovement.FinishDragBalk();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zMouseOffset;

        return _camera.ScreenToWorldPoint(mousePosition);
    }
}
