using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int _totalLevels;
    [SerializeField] private int _minCircledLevel;
    [SerializeField] private int _maxCircledLevel;
    [SerializeField] private int _bonusLevel;

    public void LoadLevel(int targetlevel)
    {
        LoadScene(targetlevel);
    }

    public void LoadBonusLevel()
    {
        SceneManager.LoadScene(_bonusLevel);
    }

    private void LoadScene(int number)
    {
        SceneManager.LoadScene(PickSceneNumber(number));
    }

    private int PickSceneNumber(int number)
    {
        if (number <= _totalLevels)
            return number;
        else
        {
            int circledNumber = _minCircledLevel;

            for (int i = _minCircledLevel; i < number; i++)
            {
                if (++circledNumber > _maxCircledLevel)
                    circledNumber = _minCircledLevel;
            }
            return circledNumber;
        }
    }
}
