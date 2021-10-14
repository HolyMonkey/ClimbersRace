using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour
{
    private const string CURRENT_LEVEL_ID = "CurrentLevelID";

    [SerializeField] private BonusGame _bonusGame;
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Character _playerCharacter;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private bool _bonusLevel = false;

    public int CurrentLevel => PlayerPrefs.GetInt(CURRENT_LEVEL_ID, 1);

    public event UnityAction LevelStarted;
    public event UnityAction BonusGameStarted;
    public event UnityAction LevelWon;
    public event UnityAction LevelLost;

    private bool _isLevelStarted = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        _playerCharacter.Dying += OnPlayerDying;
    }

    private void OnDisable()
    {
        _playerCharacter.Dying -= OnPlayerDying;
    }

    private void OnPlayerDying(Character playerCharacter)
    {
        LoseGame();
    }

    private void Update()
    {
        if (_isLevelStarted)
            return;

#if (UNITY_ANDROID && !UNITY_EDITOR)
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //-- for build
#elif (UNITY_EDITOR && UNITY_ANDROID)
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
#endif
        {
            StartLevel();
        }
    }

    public void StartBonusGame(FinishBalk finishBalk, Character character)
    {
        if (character == _playerCharacter)
        {
            _bonusGame.StartBonusGame(finishBalk);
            BonusGameStarted?.Invoke();
        }
        else
        {
            LoseGame();
            _cameraMover.ChangeTarget(character);
        }
    }

    public void NextLevel()
    {
        if (_bonusLevel)
        {
            _sceneChanger.LoadLevel(CurrentLevel);
        }
        else if (CurrentLevel % 4 == 0)
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);

            _sceneChanger.LoadBonusLevel();
        }
        else
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);

            _sceneChanger.LoadLevel(CurrentLevel);
        }
    }

    public void RestartLevel()
    {
        if (_bonusLevel)
            _sceneChanger.LoadBonusLevel();
        else
            _sceneChanger.LoadLevel(CurrentLevel);
    }

    public void StartLevel()
    {
        _isLevelStarted = true;
        LevelStarted?.Invoke();
    }

    public void WinGame()
    {
        LevelWon?.Invoke();
    }

    public void LoseGame()
    {
        LevelLost?.Invoke();
    }
}