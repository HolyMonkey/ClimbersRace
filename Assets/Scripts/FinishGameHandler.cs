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
        if (other.TryGetComponent(out CharacterInteractionHandler character))
        {
            _confetti.Play();
            _camera.SetTarget(_pointForCamera);

            StartCoroutine(PushWinner(character, _timeToStopWinner));
        }
    }

    private IEnumerator PushWinner(CharacterInteractionHandler winnerCharacter, float timeToStop)
    {
        winnerCharacter.PushInDirectionMovement(_pushForceWinner);

        yield return new WaitForSeconds(timeToStop);

        winnerCharacter.gameObject.SetActive(false);
    }
}
