using UnityEngine;
using UnityEngine.Events;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private StartTutorial _startTutorial;
    [SerializeField] private Level _level;
    [SerializeField] private UnityEvent _onLevelStarted;
    [SerializeField] private UnityEvent _onBonusStarted;
    [SerializeField] private UnityEvent _onGameWon;
    [SerializeField] private UnityEvent _onGameLost;
    [SerializeField] private UnityEvent _onPlayerMoveStarted;

    private void OnEnable()
    {
        _level.LevelStarted += () => _onLevelStarted?.Invoke();
        _startTutorial.PlayerMoveStarted += () => _onPlayerMoveStarted?.Invoke();
        _level.BonusGameStarted += () => _onBonusStarted?.Invoke();
        _level.LevelWon += () => _onGameWon?.Invoke();
        _level.LevelLost += () => _onGameLost?.Invoke();
    }
    private void OnDisable()
    {
        _level.LevelStarted -= () => _onLevelStarted?.Invoke();
        _startTutorial.PlayerMoveStarted -= () => _onPlayerMoveStarted?.Invoke();
        _level.BonusGameStarted -= () => _onBonusStarted?.Invoke();
        _level.LevelWon -= () => _onGameWon?.Invoke();
        _level.LevelLost -= () => _onGameLost?.Invoke();
    }
}
