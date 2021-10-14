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

    protected override void CollectCoin(Coin coin)
    {
        _money.AddLevelMoney(coin.RewardAmount);
        coin.Collect();
    }

    protected override void TrySetupBalkConnection(Balk balk)
    {
        if (balk.TryGetComponent(out PlayerBalkInteraction balkInteraction))
        {
            BalkConnection(balkInteraction.Balk, Character);
        }
    }
}

