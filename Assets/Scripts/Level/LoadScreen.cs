using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Level _level;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _loadingText;

    private SceneChanger _sceneChanger;
    private float _endValue = 100f;
    private float _duration = 2f;
    private float _enough = 99.6f;

    public event UnityAction ProggresBarFinished;

    private void Start()
    {
        _sceneChanger = FindObjectOfType<SceneChanger>();
        _startButton.gameObject.SetActive(false);
        StartCoroutine(AsyncLoad());
    }

    private IEnumerator AsyncLoad()
    {
        while (_progressBar.value != 1)
        {
            _progressBar.DOValue(_endValue, _duration, false).SetEase(Ease.Linear);
            _loadingText.text = string.Format("{0:0}%", _progressBar.value);

            if (_progressBar.value >= _enough)
            {
                ProggresBarFinished?.Invoke();
                _progressBar.gameObject.SetActive(false);
                _startButton.gameObject.SetActive(true);
                _loadingText.text = ("Начать");
            }
            yield return null;

        }
    }

    public void StartGame()
    {
        _sceneChanger.LoadLevel(_level.CurrentLevel);
    }
}
