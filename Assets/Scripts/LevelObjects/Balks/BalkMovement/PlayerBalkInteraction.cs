using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalkInteraction : BalkInteraction
{
    private BalkMovement _balkMovement;
    private Camera _camera;
    private CameraMover _cameraMover;

    private Vector3 _mouseOffset;
    private float _zMouseOffset;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
        _cameraMover = _camera.GetComponent<CameraMover>();
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
        float dragFOV = _balkMovement.PlayerDragBalk(newPosition);

        ScaleCamera(dragFOV);
    }

    private void OnMouseUp()
    {
        _balkMovement.FinishDragBalk();
        ScaleCamera(0);
    }

    private void ScaleCamera(float value)
    {
        _cameraMover.ScaleFOV(value);
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zMouseOffset;

        return _camera.ScreenToWorldPoint(mousePosition);
    }
}
