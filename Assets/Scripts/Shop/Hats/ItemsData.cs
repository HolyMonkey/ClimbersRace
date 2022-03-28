using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsData
{
    [SerializeField] private Item[] _items;

    public Item[] Items => _items;

    public void ChangeList(int index, string key)
    {
        if (index < 0 || index > _items.Length)
            return;

        _items[index].Buy();
        Save(key);
    }

    public void ChangeEquip(int index, string key)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].UnEquip();
        }

        _items[index].Equip();

        Save(key);
    }

    public ItemsData Load(string key)
    {
        return PlayerPrefsExtra.GetObject(key, this);
    }

    private void Save(string key)
    {
        PlayerPrefsExtra.SetObject(key, this);
    }
}
