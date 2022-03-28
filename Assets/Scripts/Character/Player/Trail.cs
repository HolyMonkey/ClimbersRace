using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem.Play();
    }

    public void Change(ParticleSystem prefab)
    {
        Instantiate(prefab);
    }
}
