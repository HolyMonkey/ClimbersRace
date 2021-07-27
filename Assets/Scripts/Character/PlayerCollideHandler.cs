using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollideHandler : MonoBehaviour
{
    [SerializeField] private CharacterInteractionHandler _character;

    public event UnityAction<Vector3> KnockedDownEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterInteractionHandler enemy))
        {
            if (enemy.IsAttachingBalk)
            {
                KnockedDownEnemy?.Invoke(transform.position);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerBalk playerBalk))
        {
            _character.AttachToBalk(playerBalk);
        }
    }
}
