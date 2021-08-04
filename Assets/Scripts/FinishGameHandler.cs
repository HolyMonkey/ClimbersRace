using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private Transform _pointForCamera;
    [SerializeField] private CameraMover _camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            _confetti.Play();
        }
    }
}
