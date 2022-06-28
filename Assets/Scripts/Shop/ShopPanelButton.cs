using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image), typeof(Button))]
public class ShopPanelButton : MonoBehaviour
{
    [SerializeField] private int _index;

    private Button _button;

    public Button Button => _button;

    public event UnityAction<int> ButtonClick; 

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClick?.Invoke(_index);
    }
}
