using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementScaller : MonoBehaviour
{
    [SerializeField] private Vector3 _endVector;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    private void Start()
    {
        transform.DOScale(_endVector, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(_ease);
    }
}