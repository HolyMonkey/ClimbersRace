using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BalkMover))]
public class PlayerBalk : Balk
{
    public BalkMover BalkMover { get; private set; }

    private void Awake()
    {
        BalkMover = GetComponent <BalkMover>();
    }
}
