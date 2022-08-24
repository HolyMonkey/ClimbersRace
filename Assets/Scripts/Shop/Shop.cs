using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemsConteiner _hatsConteiner;
    [SerializeField] private ItemsConteiner _skinsConteiner;
    [SerializeField] private ItemsConteiner _shortsConteiner;
    [SerializeField] private ItemsConteiner _trailsConteiner;

    public event UnityAction<int> SoldHat;
    public event UnityAction<int> SoldSkin;
    public event UnityAction<int> SoldShort;
    public event UnityAction<int> SoldTrail;

    private void OnEnable()
    {
        _hatsConteiner.ChangeItem += OnHatSold;
        _skinsConteiner.ChangeItem += OnSkinSold;
        _shortsConteiner.ChangeItem += OnShortSold;
        _trailsConteiner.ChangeItem += OnTrailSold;
    }

    private void OnDisable()
    {
        _hatsConteiner.ChangeItem -= OnHatSold;
        _skinsConteiner.ChangeItem -= OnSkinSold;
        _shortsConteiner.ChangeItem -= OnShortSold;
        _trailsConteiner.ChangeItem -= OnTrailSold;
    }

    private void OnHatSold(int numberItem)
    {
        SoldHat?.Invoke(numberItem);
    }

    private void OnSkinSold(int numberItem)
    {
        SoldSkin?.Invoke(numberItem);
    }

    private void OnShortSold(int numberItem)
    {
        SoldShort?.Invoke(numberItem);
    }

    private void OnTrailSold(int numberItem)
    {
        SoldTrail?.Invoke(numberItem);
    }
}
