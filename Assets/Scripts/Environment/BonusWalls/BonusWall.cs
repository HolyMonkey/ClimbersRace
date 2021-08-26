using UnityEngine;

public class BonusWall : MonoBehaviour
{
    [SerializeField] private int _multiplier;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _playerMoveDuration =2f;

    public Vector3 TargetPoint =>_targetPoint.position;
    public float PlayerMoveDuration => _playerMoveDuration;
    public int BonusMultiplier => _multiplier;
}