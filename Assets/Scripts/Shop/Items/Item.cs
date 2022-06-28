using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    [SerializeField] private string _name;
    [SerializeField] private bool _isBuyed;
    [SerializeField] private bool _isEquiped;

    public string Name => _name;
    public bool IsBuyed => _isBuyed;
    public bool IsEquiped => _isEquiped;

    public void Buy()
    {
        this._isBuyed = true;
    }

    public void Equip()
    {
        this._isEquiped = true;
    }

    public void UnEquip()
    {
        this._isEquiped = false;
    }
}
