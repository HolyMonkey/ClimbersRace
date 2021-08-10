using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineSmoother
{
    public static Vector3[] SmoothLine(Vector3[] inputPoints, float segmentSize)
    {
        AnimationCurve curveX = new AnimationCurve();
        AnimationCurve curveY = new AnimationCurve();
        AnimationCurve curveZ = new AnimationCurve();

        Keyframe[] keysX = new Keyframe[inputPoints.Length];
        Keyframe[] keysY = new Keyframe[inputPoints.Length];
        Keyframe[] keysZ = new Keyframe[inputPoints.Length];

        for (int i = 0; i < inputPoints.Length; i++)
        {
            keysX[i] = new Keyframe(i, inputPoints[i].x);
            keysY[i] = new Keyframe(i, inputPoints[i].y);
            keysZ[i] = new Keyframe(i, inputPoints[i].z);
        }

        curveX.keys = keysX;
        curveY.keys = keysY;
        curveZ.keys = keysZ;

        for (int i = 0; i < inputPoints.Length; i++)
        {
            curveX.SmoothTangents(i, 0);
            curveY.SmoothTangents(i, 0);
            curveZ.SmoothTangents(i, 0);
        }

        List<Vector3> lineSegments = new List<Vector3>();

        for (int i = 0; i < inputPoints.Length; i++)
        {
            lineSegments.Add(inputPoints[i]);

            if (i + 1 < inputPoints.Length)
            {
                float distanceToNext = Vector3.Distance(inputPoints[i], inputPoints[i + 1]);
                int segments = (int)(distanceToNext / segmentSize);

                for (int s = 1; s < segments; s++)
                {
                    float time = ((float)s / (float)segments) + (float)i;

                    Vector3 newSegment = new Vector3(curveX.Evaluate(time), curveY.Evaluate(time), curveZ.Evaluate(time));

                    lineSegments.Add(newSegment);
                }
            }
        }

        return lineSegments.ToArray();
    }
}
