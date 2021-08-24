using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusScaleView : MonoBehaviour
{
    [SerializeField] private RectTransform _arrowBase;
    [SerializeField] private RangeFloat _arrowMinMaxAngle;

    internal void ChangeValue(float value)
    {
        float zRotation = Mathf.Lerp(_arrowMinMaxAngle.Min, _arrowMinMaxAngle.Max, value);

        _arrowBase.localRotation = Quaternion.Euler(0, 0, zRotation);
    }
}
