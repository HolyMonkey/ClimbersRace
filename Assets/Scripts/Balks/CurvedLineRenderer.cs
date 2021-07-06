using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class CurvedLineRenderer : MonoBehaviour
{
    [SerializeField] private float lineSegmentSize = 0.15f;
    [SerializeField] private float lineWidth = 0.1f;
    [Header("Gizmos")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private float gizmoSize = 0.1f;
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.5f);

    private CurvedLinePoint[] linePoints = new CurvedLinePoint[0];
    private Vector3[] linePositions = new Vector3[0];
    private Vector3[] linePositionsOld = new Vector3[0];

    public void Update()
    {
        GetPoints();
        SetPointsToLine();
    }

    private void GetPoints()
    {
        linePoints = GetComponentsInChildren<CurvedLinePoint>();

        linePositions = new Vector3[linePoints.Length];
        for (int i = 0; i < linePoints.Length; i++)
        {
            linePositions[i] = linePoints[i].transform.position;
        }
    }

    private void SetPointsToLine()
    {
        if (linePositionsOld.Length != linePositions.Length)
        {
            linePositionsOld = new Vector3[linePositions.Length];
        }

        bool moved = false;
        for (int i = 0; i < linePositions.Length; i++)
        {
            if (linePositions[i] != linePositionsOld[i])
            {
                moved = true;
            }
        }

        if (moved == true)
        {
            LineRenderer line = this.GetComponent<LineRenderer>();

            Vector3[] smoothedPoints = LineSmoother.SmoothLine(linePositions, lineSegmentSize);

            line.SetVertexCount(smoothedPoints.Length);
            line.SetPositions(smoothedPoints);
            line.SetWidth(lineWidth, lineWidth);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Update();
    }

    private void OnDrawGizmos()
    {
        if (linePoints.Length == 0)
        {
            GetPoints();
        }

        foreach (CurvedLinePoint linePoint in linePoints)
        {
            linePoint.showGizmo = showGizmos;
            linePoint.gizmoSize = gizmoSize;
            linePoint.gizmoColor = gizmoColor;
        }
    }
}
