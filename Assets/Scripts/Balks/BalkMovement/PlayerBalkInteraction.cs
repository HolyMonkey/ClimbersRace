using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalkInteraction : MonoBehaviour
{
    [SerializeField] private BalkInteractionHandler _balkInteraction;
    [SerializeField] private float _minDragDistanceToJump;

    private Camera _camera;
    private CameraMover _cameraMover;
    private Vector3 _offset;
    private float _zOffset = 5f;
    private Vector3 _startDragPosition;

    private void Start()
    {
        _camera = Camera.main;
        _cameraMover = _camera.GetComponent<CameraMover>();

        //_zOffset = _camera.WorldToScreenPoint(transform.localPosition).z;
    }

    private void OnMouseDown()
    {
        _offset = transform.position - GetMousePosition();
        _startDragPosition = transform.position;
        _balkInteraction.BeginDragBalk();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMousePosition() + _offset;

        _balkInteraction.DragBalk(newPosition);
    }

    private void OnMouseUp()
    {
        if (Vector3.Distance(_startDragPosition, transform.position) > _minDragDistanceToJump)
            _balkInteraction.FinishDragBalk();

    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zOffset;

        return _camera.ScreenToWorldPoint(mousePosition);
    }
}
