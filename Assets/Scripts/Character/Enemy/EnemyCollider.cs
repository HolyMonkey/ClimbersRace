using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : CharacterCollider
{
    [SerializeField] private Character _characterInteractionHandler;

    protected override bool CheckBalkType(out Balk balk, Collider collider)
    {
        if (collider.TryGetComponent(out EnemyBalk enemyBalk))
        {
            balk = enemyBalk;
            return true;
        }
        else
        {
            balk = null;
            return false;
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
