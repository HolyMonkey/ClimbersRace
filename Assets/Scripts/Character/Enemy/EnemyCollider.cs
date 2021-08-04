using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private Character _characterInteractionHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyBalk enemyBalk))
        {
            _characterInteractionHandler.AttachToBalk(enemyBalk);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyBalk enemyBalk))
        {
            _characterInteractionHandler.DetachFromBalk();
        }
    }
}
