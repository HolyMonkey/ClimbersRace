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

    protected override void CollectCoin(Coin coin)
    {
        coin.Collect();
    }

    protected override void TrySetupBalkConnection(Balk balk)
    {
        if (balk.TryGetComponent(out EnemyBalkInteraction enemyBalkInteraction))
        {
            BalkConnection(enemyBalkInteraction.Balk, Character);
            _enemyAI.SetCurrentNode(enemyBalkInteraction.BalkAINode);
        }
    }
}