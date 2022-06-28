using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _soldText;
    [SerializeField] private Image _equipIcon;
    [SerializeField] private Image _lockIcon;

    [SerializeField] private int _price = 0;
    [SerializeField] private bool _isSold = false;

    private Button _button;
    private ItemsConteiner _shop;
    private Animator _IsNotEnoughMoney;
    private Color _unFade = new Color(1f, 1f, 1f, 1f);
    private Color _fade = new Color(1f, 1f, 1f, 0f);
    private const string _triggerTextAnimationNotMoney = "IsLock";
    private float _duration = 1f;

    public int Price => _price;
    public bool IsSold => _isSold;

    public event UnityAction<ItemView> OnSellButtonClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _shop = GetComponentInParent<ItemsConteiner>();
        _IsNotEnoughMoney = _lockIcon.GetComponent<Animator>();
        _priceText.text = _price.ToString();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _shop.Sold += OnSold;
        _shop.FailledoOnSell += OnFailledonSell;
        _shop.Equiped += OnEquip;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _shop.Sold -= OnSold;
        _shop.FailledoOnSell -= OnFailledonSell;
        _shop.Equiped -= OnEquip;
    }

    private void OnSold(ItemView item)
    {
        if (item.IsSold == false)
        {
            item._lockIcon.color = _fade;
            item._priceText.alpha = 0f;
            item._soldText.alpha = 1f;
            item._isSold = true;
        }
    }

    private void OnFailledonSell(ItemView item)
    {
        StartCoroutine(ChangeColor(item._priceText));
        item._IsNotEnoughMoney.SetTrigger(_triggerTextAnimationNotMoney);
    }

    private void OnEquip(ItemView item)
    {
        FadeEquip();
        item._equipIcon.color = _unFade;
    }

    private void FadeEquip()
    {
        _equipIcon.color = _fade;
    }

    private IEnumerator ChangeColor(TMP_Text text)
    {
        Cursor.lockState = CursorLockMode.Locked;

        text.DOColor(Color.red, _duration).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
        yield return new WaitForSeconds(_duration);

        Cursor.lockState = CursorLockMode.None;
    }

    private void OnButtonClick()
    {
        OnSellButtonClick?.Invoke(this);
    }
}