using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SmoothedTextValue _smoothTextValue;
    [SerializeField] private Money _money;

    private int _currentValue = -1;

    private void OnEnable()
    {
        _money.MoneyChanged += OnNewCountMoney;
        _money.PullEvent();
    }

    private void OnDisable()
    {
        _money.MoneyChanged -= OnNewCountMoney;
    }

    private void OnNewCountMoney(int value)
    {
        if (_currentValue == -1)
            _currentValue = value;

        _smoothTextValue.StartSmooth(_text, _currentValue, value);
        _currentValue = value;
    }
}
