using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWall : MonoBehaviour
{
    [SerializeField] private int _multiplier;
    [SerializeField] private Transform _targetPoint;

    public Transform TargetPointTransform =>_targetPoint;
}
