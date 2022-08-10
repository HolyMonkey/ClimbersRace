using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool IsOn = true;

    public void Set(bool value)
    {
        IsOn = value;
    }
}
