using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private SceneChanger _sceneChanger;

    private void Start()
    {
        _sceneChanger.LoadLevel(_level.CurrentLevel);
    }
}
