using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private TMP_Text _text;

    private string[] _items = new string[51];
    private void Start()
    {
        _text.text = "Выбор Уровня";
        string name = "Уровень";
        int number = 1;
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i] = ($"{name} {number}");
            number++;
            _dropdown.options.Add(new TMP_Dropdown.OptionData(_items[i]));
        }
    }

    public void DropDownIndexChanged(int index)
    {
        _text.text = _items[index];
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(_dropdown.value + 2);
    }
}
