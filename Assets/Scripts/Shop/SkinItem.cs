using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkinItem
{
    [SerializeField] private Material _skin;
    [SerializeField] private bool _isEquiped;

    public Material Skin => _skin;
    public bool IsEquiped => _isEquiped;

    public void Equip()
    {
        this._isEquiped = true;
    }

    public void UnEquip()
    {
        this._isEquiped = false;
    }
}
