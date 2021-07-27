using UnityEngine;
using System.Collections;

public class CurvedLinePoint : MonoBehaviour 
{
	private bool _showGizmos = true;
	private float _gizmosSize = 0.1f;
	private Color _gizmosColor = new Color(1,0,0,0.5f);

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
