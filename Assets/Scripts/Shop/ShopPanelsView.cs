using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelsView : MonoBehaviour
{
    [SerializeField] private ShopPanelButton[] _buttons;
    [SerializeField] private ShopPanel[] _panels;
    [SerializeField] private Sprite _spriteDefault;
    [SerializeField] private Sprite _spriteRed;

    private void OnEnable()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].ButtonClick += OnButtonClick;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].ButtonClick -= OnButtonClick;
        }
    }

    public void OnButtonClick(int value)
    {
        if (value > _panels.Length || value < 0)
            return;

        ChangeActivePanel(value);
        ChangeSprite(value);
    }

    private void ChangeSprite(int value)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i == value)
                _buttons[i].Button.image.sprite = _spriteRed;
            else
                _buttons[i].Button.image.sprite = _spriteDefault;
        }
    }

    private void ChangeActivePanel(int value)
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            if (i == value)
                _panels[i].gameObject.SetActive(true);
            else
                _panels[i].gameObject.SetActive(false);
        }
    }
}