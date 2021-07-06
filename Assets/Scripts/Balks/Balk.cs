using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Balk : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
