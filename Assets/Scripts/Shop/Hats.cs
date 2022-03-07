using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hat", menuName = "Shop/Create new hat", order = 51)]
public class Hats : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    public GameObject Prefab => _prefab;
}
