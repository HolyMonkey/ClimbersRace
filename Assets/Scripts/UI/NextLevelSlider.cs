using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class NextLevelSlider : MonoBehaviour
{
    private const string SLIDER_VALUE = "SliderValue";
    private Slider _slider;
    private int _value = 2;
    private int _fullSlider = 8;

    public int SliderValue
    {
        get { return PlayerPrefs.GetInt(SLIDER_VALUE, 0); }
        private set { PlayerPrefs.SetInt(SLIDER_VALUE, value); }
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = SliderValue;
    }

    public void ChangeValue()
    {
        if (_slider.value == _fullSlider)
        {
            SliderValue = 0;
            _slider.value = SliderValue;
        }
        else
        {
            SliderValue += _value;
            _slider.value += SliderValue;
        }
    }
}
