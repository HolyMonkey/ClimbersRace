using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemsConteiner : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private Money _money;
    [SerializeField] private ItemsData _itemsData;

    private ItemView[] _items;

    public event UnityAction<ItemView> Sold;
    public event UnityAction<ItemView> FailledoOnSell;
    public event UnityAction<ItemView> Equiped;

    public event UnityAction<int> ChangeItem;

    private void Awake()
    {
        _items = GetComponentsInChildren<ItemView>();

        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].OnSellButtonClick += OnCheckItem;
        }
    }

    private void Start()
    {
        _itemsData = _itemsData.Load(_key);

        for (int i = 0; i < _itemsData.Items.Length; i++)
        {
            if (_itemsData.Items[i].IsEquiped == true)
                Equiped?.Invoke(_items[i]);

            if (_itemsData.Items[i].IsBuyed == true)
                Sold?.Invoke(_items[i]);
        }
    }

    private void OnCheckItem(ItemView item)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == item)
                TrySellItem(item);
        }
    }

    private void TrySellItem(ItemView itemTarget)
    {
        if (itemTarget.IsSold == false)
        {
            if (_money.TryRemoveMoney(itemTarget.Price))
                ChangeItemData(itemTarget);
            else
                FailledoOnSell?.Invoke(itemTarget);
        }
        else
        {
            EquipItem(itemTarget);
        }
    }

    private void ChangeItemData(ItemView itemTraget)
    {
        int itemNumber = 0;

        foreach (var item in _items)
        {
            if (item == itemTraget)
                _itemsData.ChangeList(itemNumber, _key);

            itemNumber++;
        }

        Sold?.Invoke(itemTraget);
    }

    private void EquipItem(ItemView itemTarget)
    {
        int itemNumber = 0;

        foreach (var item in _items)
        {
            if (item == itemTarget)
            {
                ChangeItem?.Invoke(itemNumber);
                Equiped?.Invoke(itemTarget);
                _itemsData.ChangeEquip(itemNumber, _key);
            }

            itemNumber++;
        }
    }
}