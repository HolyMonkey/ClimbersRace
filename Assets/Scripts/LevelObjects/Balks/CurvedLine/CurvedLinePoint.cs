using UnityEngine;

public class CurvedLinePoint : MonoBehaviour
{
    private bool _showGizmos = true;
    private float _gizmosSize = 0.02f;
    private Color _gizmosColor = new Color(0.7f, 0.7f, 0, 0.8f);

    private void OnDrawGizmos()
    {
        if (_showGizmos == true)
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(transform.position, _gizmosSize);
        }
    }

    public void SetShowGizmos(bool showGizmos)
    {
        _showGizmos = showGizmos;
    }
}
