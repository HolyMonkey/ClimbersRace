using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _loadingText;

    private float _endValue = 100f;
    private float _duration = 5f;
    private float _enough = 99.6f;
    
    private void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    private IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_level.CurrentLevel);
        operation.allowSceneActivation = false;
        bool isEndLoaded = false;
        while (!operation.isDone && _progressBar.value != 1)
        {
            _progressBar.DOValue(_endValue, _duration, false).SetEase(Ease.Linear);
            _loadingText.text = string.Format("{0:0}%", _progressBar.value);

            if (_progressBar.value >= _enough) 
                isEndLoaded = true;

            if (isEndLoaded && !operation.allowSceneActivation)
                    operation.allowSceneActivation = true;

            yield return null;

        }
    }
}
