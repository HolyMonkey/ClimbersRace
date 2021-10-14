using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private AmplitudeAnalytics _amplitudeAnalytics;

    private void Start()
    {
        _amplitudeAnalytics.GameStart();
        _sceneChanger.LoadLevel(_level.CurrentLevel);
    }
}
