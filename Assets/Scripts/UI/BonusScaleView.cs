using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusScaleView : MonoBehaviour
{
    [SerializeField] private RectTransform _arrowBase;
    [SerializeField] private float _arrowMaxAngle = 90f;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    internal void ChangeValue(float value)
    {
        float zRotation = Mathf.Lerp(0, _arrowMaxAngle, value);

        _arrowBase.rotation = Quaternion.Euler(0, 0, -zRotation);
    }
}
