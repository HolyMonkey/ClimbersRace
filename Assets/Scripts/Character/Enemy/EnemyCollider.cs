using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemyCollider : CharacterCollider
{
    private EnemyAI _enemyAI;

    protected override void Awake()
    {
        base.Awake();
        _enemyAI = GetComponent<EnemyAI>();
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (collider.TryGetComponent(out Coin coin))
        {
            coin.Collect();
        }
    }

    protected override void TrySetupBalkConnection(Collider collider)
    {
        if (collider.TryGetComponent(out EnemyBalkInteraction enemyBalkInteraction))
        {
            TryConnection(enemyBalkInteraction.Balk, Character);
            _enemyAI.CurrentNode = enemyBalkInteraction.BalkAINode;
        }
    }
}