using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _target;
    [SerializeField] private float _duration = 5f;

    private void Start()
    {
        transform.DOMove(_target, _duration).SetLoops(-1, LoopType.Yoyo);
    }
}
