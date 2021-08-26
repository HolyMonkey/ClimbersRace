using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _rewardAmount = 1;
    [SerializeField] private ParticleSystem _collectParticle;

    public int RewardAmount => _rewardAmount;

    public void Collect()
    {
        _collectParticle.transform.parent = null;
        _collectParticle.Play();

        Destroy(gameObject);
    }
}
