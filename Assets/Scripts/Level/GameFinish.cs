using UnityEngine;

public class GameFinish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private Transform _finishWallForRotation;
    [SerializeField] private Level _level;

    private void OnEnable()
    {
        _level.LevelWon += OnWinGame;
    }

    private void OnDisable()
    {
        _level.LevelWon -= OnWinGame;
    }

    private void OnWinGame()
    {
        _confetti.Play();
    }

    public void RotateFinishWall(Vector3 targetLookAt)
    {
        if (_finishWallForRotation)
        {
            Vector3 direction = _finishWallForRotation.position - targetLookAt;
            direction.y = 0;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _finishWallForRotation.rotation = lookRotation;
            _confetti.transform.rotation = lookRotation;
        }
    }

    internal void SetFinishWall(BonusWall targetWall)
    {
        _confetti.transform.position = targetWall.TargetPoint;
    }
}
