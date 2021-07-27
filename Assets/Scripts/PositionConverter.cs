using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionConverter
{
    public Vector2 ConvertWorldPositionToCanvasPosition(Canvas parentCanvas, Vector3 worldPosition)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, screenPosition, parentCanvas.worldCamera, out Vector2 movePosition);

        return parentCanvas.transform.TransformPoint(movePosition);
    }
}
