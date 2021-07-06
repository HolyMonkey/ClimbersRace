using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class JointRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _connectionPoint;

    private LineRenderer _jointRenderer;

    private void Start()
    {
        _jointRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RenderJoint();
    }

    private void RenderJoint()
    {
        RaycastHit hit = RaycastForward();

        _jointRenderer.SetPosition(0, transform.position);
        _jointRenderer.SetPosition(1, _connectionPoint.transform.position);
    }

    private RaycastHit RaycastForward()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity))
        {
            return hit;
        }

        return new RaycastHit();
    }
}
