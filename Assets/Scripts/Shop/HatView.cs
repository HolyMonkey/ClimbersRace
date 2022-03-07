using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatView : MonoBehaviour
{
    [SerializeField] private Hats _hat;
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        _prefab = _hat.Prefab;
    }
}
