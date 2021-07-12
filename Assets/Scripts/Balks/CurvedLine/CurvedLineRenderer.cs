using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class CurvedLineRenderer : MonoBehaviour
{
    [SerializeField] private float _lineSegmentSize = 0.15f;
    [SerializeField] private float _lineWidth = 0.1f;
    [SerializeField] private bool _showGizmos = true;

    private LineRenderer _lineRenderer;
    private CurvedLinePoint[] _linePoints;
    private Vector3[] _linePositions = new Vector3[0];
    private Vector3[] _linePositionsOld = new Vector3[0];

    private void OnValidate()
    {
        if (Application.isPlaying && _linePoints.Length != 0)
        {
            foreach (CurvedLinePoint linePoint in _linePoints)
            {
                linePoint.SetShowGizmos(_showGizmos);
            }
        }
    }

    private void Awake()
    {
        _linePoints = GetComponentsInChildren<CurvedLinePoint>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Render();
    }

    public void Render()
    {
        GetPointsPosition();
        SetPointsToLine();
    }

    private void GetPointsPosition()
    {
        _linePositions = new Vector3[_linePoints.Length];

        for (int i = 0; i < _linePositions.Length; i++)
        {
            _linePositions[i] = _linePoints[i].transform.position;
        }
    }

    private void SetPointsToLine()
    {
        bool moved = false;

        if (_linePositionsOld.Length != _linePositions.Length)
        {
            _linePositionsOld = new Vector3[_linePositions.Length];
        }

        for (int i = 0; i < _linePositions.Length; i++)
        {
            if (_linePositions[i] != _linePositionsOld[i])
            {
                moved = true;
            }
        }

        if (moved == true)
        {
            Vector3[] smoothedPoints = LineSmoother.SmoothLine(_linePositions, _lineSegmentSize);

            _lineRenderer.positionCount = smoothedPoints.Length;
            _lineRenderer.SetPositions(smoothedPoints);
            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;
        }
    }
}
