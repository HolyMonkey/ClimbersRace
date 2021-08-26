using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] Level _level;
    [SerializeField] GameObject _startText;

    private void Awake()
    {
        if (_level && _level.CurrentLevel != 1)
        {
            gameObject.SetActive(false);
            _startText.SetActive(true);
        }
    }
}
