using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPathActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] _coinsPaths;

    void Start()
    {
        GameObject randomPath = _coinsPaths[Random.Range(0, _coinsPaths.Length)];
        randomPath.SetActive(true);
    }
}
