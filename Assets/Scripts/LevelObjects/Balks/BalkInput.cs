using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BalkInput : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private BalkMovement _balkMovement;
    [SerializeField] private PlayerInput _playerInput;

    private Camera _camera;
    private CameraMover _cameraMover;

    private Vector3 _mouseOffset;
    private float _zMouseOffset;

    public event UnityAction PlayerStartMoved;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _camera = Camera.main;
        _cameraMover = _camera.GetComponent<CameraMover>();
        _playerInput = FindObjectOfType<PlayerInput>();
    }

    private void OnMouseDown()
    {
        if (!_playerInput.IsOn)
            return;

        _zMouseOffset = _camera.WorldToScreenPoint(transform.position).z;
        _mouseOffset = transform.position - GetMousePosition();
        _balkMovement.BeginDragBalk();
        PlayerStartMoved?.Invoke();
    }

    private void OnMouseDrag()
    {
        if (!_playerInput.IsOn)
            return;

        Vector3 newPosition = GetMousePosition() + _mouseOffset;
        float dragFOV = _balkMovement.PlayerDragBalk(newPosition);

        ScaleCamera(dragFOV);
    }

    private void OnMouseUp()
    {
        if (!_playerInput.IsOn)
            return;

        _balkMovement.FinishDragBalk();
        _audio.Play();
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
