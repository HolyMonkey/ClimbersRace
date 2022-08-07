using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private Money _money;
    [SerializeField] private Level _level;
<<<<<<< HEAD
    [SerializeField] private TMP_Text _rewardCountText;
    [SerializeField] private bool _withReward;
    [SerializeField] private Advertisement _advertisement;
=======
    [SerializeField] private Advertisement _advertisement;
    [SerializeField] private TMP_Text _multiplierText;
    [SerializeField] private TMP_Text _rewardCountText;
    [SerializeField] private bool _withReward;
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
    
    private Button _button;

    private void Awake()
    {
        _money = FindObjectOfType<Money>();
        _level = FindObjectOfType<Level>();
        _button = GetComponent<Button>();
        _money = FindObjectOfType<Money>();
        _level = FindObjectOfType<Level>();
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

    private void OnLevelIncomeReady(int currentMoney, int levelMultiplier)
    {
<<<<<<< HEAD
        multiplier = _withReward ? multiplier * _advertisement.AdMultiplier : multiplier;
        _rewardCountText.text = (currentMoney * multiplier).ToString();
=======
        int reward = currentMoney * levelMultiplier;
        reward *= _withReward ? _advertisement.AdBonusMultiplier : 1;
        _rewardCountText.text = (reward).ToString();
        if(_multiplierText)
            _multiplierText.text = "x" + _advertisement.AdBonusMultiplier.ToString();
>>>>>>> 9e0ee8453a943003ef99071209113a25f4742522
    }

    private void OnButtonClick()
    {
        if(!_withReward)
        {
            _money.RecieveLevelBonus();
            _level.NextLevel();
        }
    }
}
