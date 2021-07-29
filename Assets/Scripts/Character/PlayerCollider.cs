using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private CharacterInteraction _character;

    public event UnityAction<Vector3> KnockedDownEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterInteraction enemy))
        {
            if (enemy.IsAttachingBalk)
            {
                KnockedDownEnemy?.Invoke(transform.position);
            }
        }

        if (other.TryGetComponent(out PlayerBalk playerBalk))
        {
            _character.AttachToBalk(playerBalk);
        }
    }
}
