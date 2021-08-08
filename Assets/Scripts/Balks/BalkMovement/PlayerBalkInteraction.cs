using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BalkInteractionHandler))]
public class PlayerBalkInteraction : MonoBehaviour
{
    private BalkInteractionHandler _balkInteraction;

    private Camera _camera;

    private Vector3 _mouseOffset;
    private float _zMouseOffset;

    private void Awake()
    {
        _camera = Camera.main;
        _balkInteraction = GetComponent<BalkInteractionHandler>();
    }

    private void OnMouseDown()
    {
        _zMouseOffset = _camera.WorldToScreenPoint(transform.position).z;
        _mouseOffset = transform.position - GetMousePosition();
        _balkInteraction.BeginDragBalk();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMousePosition() + _mouseOffset;
        _balkInteraction.DragBalk(newPosition);
    }

    private void OnMouseUp()
    {
        _balkInteraction.FinishDragBalk();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zMouseOffset;

        return _camera.ScreenToWorldPoint(mousePosition);
    }
}
