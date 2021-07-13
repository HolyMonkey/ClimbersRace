using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private Transform _pointForCamera;
    [SerializeField] private OrbitCamera _camera;

    private float _timeToStopWinner = 1f;
    private float _pushForceWinner = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MovementHandler movement))
        {
            _confetti.Play();
            _camera.SetTarget(_pointForCamera);

            StartCoroutine(PushWinner(movement, _timeToStopWinner));
        }
    }

    private IEnumerator PushWinner(MovementHandler winnerMovement, float timeToStop)
    {
        winnerMovement.Push(Vector3.up * _pushForceWinner);

        yield return new WaitForSeconds(timeToStop);

        winnerMovement.gameObject.SetActive(false);
    }
}
