using UnityEngine;

[RequireComponent(typeof(Money))]
public class PlayerCollider : CharacterCollider
{
    [SerializeField] private AudioSource _audioSource;

    private Money _money;

    protected override void Awake()
    {
        base.Awake();
        _money = GetComponent<Money>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void CollectCoin(Coin coin)
    {
        _money.AddLevelMoney(coin.RewardAmount);
        _audioSource.Play();
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

