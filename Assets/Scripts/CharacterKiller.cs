using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CharacterKiller : MonoBehaviour
{
    private BoxCollider _killingBox;

    private void Start()
    {
        _killingBox = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
