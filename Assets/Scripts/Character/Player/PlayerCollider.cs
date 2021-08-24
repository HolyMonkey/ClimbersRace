using UnityEngine;

[RequireComponent(typeof(Money))]
public class PlayerCollider : CharacterCollider
{
    private Money _money;

    protected override void Awake()
    {
        base.Awake();
        _money = GetComponent<Money>();
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (collider.TryGetComponent(out Coin coin))
        {
            _money.AddLevelMoney(coin.RewardAmount);
            coin.Collect();
        }
    }

    protected override void TrySetupBalkConnection(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerBalkInteraction balkInteraction))
        {
            TryConnection(balkInteraction.Balk, Character);
        }
    }
}

