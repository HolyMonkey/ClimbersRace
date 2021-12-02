using UnityEngine;
using UnityEngine.Events;

public class BalkInput : MonoBehaviour
{
    [SerializeField] private BalkMovement _balkMovement;
    [SerializeField] private ParticleSystem _particle;

    private const string FIRST_TIME_OPENING = "FirstTimeOpening";

    private Camera _camera;
    private CameraMover _cameraMover;

    private Vector3 _mouseOffset;
    private float _zMouseOffset;

    public event UnityAction PlayerStartMoved;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(FIRST_TIME_OPENING, 1) == 1)
        {
            _particle.gameObject.SetActive(true);

            PlayerPrefs.SetInt(FIRST_TIME_OPENING, 0);
        }

        _camera = Camera.main;
        _cameraMover = _camera.GetComponent<CameraMover>();
    }

    private void OnMouseDown()
    {
        _zMouseOffset = _camera.WorldToScreenPoint(transform.position).z;
        _mouseOffset = transform.position - GetMousePosition();
        _balkMovement.BeginDragBalk();
        PlayerStartMoved?.Invoke();

        if (_particle == null)
            return;
        else
            _particle.gameObject.SetActive(false);

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
