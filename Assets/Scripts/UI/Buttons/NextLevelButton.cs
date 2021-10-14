using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private Money _money;
    [SerializeField] private Level _level;
    [SerializeField] private TMP_Text _multiplierText;
    [SerializeField] private TMP_Text _rewardCountText;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _money.LevelIncomeReady += OnLevelIncomeReady;
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _money.LevelIncomeReady -= OnLevelIncomeReady;
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnLevelIncomeReady(int currentMoney, int multiplier)
    {
        _rewardCountText.text = (currentMoney * multiplier).ToString();
        _multiplierText.text = "x" + multiplier.ToString();
    }

    private void OnButtonClick()
    {
        _money.RecieveLevelBonus();
        _level.NextLevel();
    }
}
