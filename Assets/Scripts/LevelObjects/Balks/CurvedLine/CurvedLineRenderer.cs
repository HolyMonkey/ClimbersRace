using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CurvedLineRenderer : MonoBehaviour
{
    [SerializeField] private float _lineSegmentSize = 0.15f;
    [SerializeField] private float _lineWidth = 0.1f;
    [SerializeField] private CurvedLinePoint[] _linePoints;

    private LineRenderer _lineRenderer;

    private Vector3[] _linePositions;
    private Vector3[] _basicLinePositions;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        SetBasicsPoints();
        SetLineWidth();
    }

    private void Start()
    {
        RenderSmoothLine();
    }

    private void Update()
    {
        if (CheckMovingPoints())
            RenderSmoothLine();
    }

    private void SetBasicsPoints()
    {
        _linePositions = new Vector3[_linePoints.Length];
        _basicLinePositions = new Vector3[_linePoints.Length];

        for (int i = 0; i < _linePoints.Length; i++)
        {
            _basicLinePositions[i] = _linePoints[i].transform.position;
        }
    }

    private void SetLineWidth()
    {
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;
    }

    private bool CheckMovingPoints()
    {
        if ((_linePoints[_linePoints.Length - 1].transform.position - _basicLinePositions[_basicLinePositions.Length - 1]).sqrMagnitude > 0.001f)
            return true;
        return false;
    }

    private void RenderSmoothLine()
    {
        GetUpdatedPoints();

        Vector3[] smoothedPoints = LineSmoother.SmoothLine(_linePositions, _lineSegmentSize);

        _lineRenderer.positionCount = smoothedPoints.Length;
        _lineRenderer.SetPositions(smoothedPoints);
    }

    private void GetUpdatedPoints()
    {
        for (int i = 0; i < _linePositions.Length; i++)
        {
            _linePositions[i] = _linePoints[i].transform.position;
        }
    }
}
