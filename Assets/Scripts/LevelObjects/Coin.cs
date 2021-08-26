using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _rewardAmount = 1;

    public int RewardAmount => _rewardAmount;

    public void Collect()
    {
        Destroy(gameObject);
    }
}
