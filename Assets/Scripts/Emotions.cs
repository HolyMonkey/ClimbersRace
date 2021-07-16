using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Emotions : MonoBehaviour
{
    [SerializeField] private Transform _emotion;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private float _appearanceDuration;
    [SerializeField] private float _disappearanceDuration;
    [SerializeField] private Vector2 _endDistance;
    [SerializeField] private Vector2 _offset;

    private Vector3 _baseEmotionScale;

    private void Awake()
    {
        _baseEmotionScale = _emotion.localScale;
        _emotion.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _player.KnockedDownEnemy += OnKnockedDownEnemy;
    }

    private void OnDisable()
    {
        _player.KnockedDownEnemy -= OnKnockedDownEnemy;
    }

    private void OnKnockedDownEnemy(Vector3 position)
    {
        PositionConverter converter = new PositionConverter();

        Vector3 offset = new Vector3(_offset.x, _offset.y);
        Vector2 emotionPosition = converter.ConvertWorldPositionToCanvasPosition(_canvas, position + offset);

        ShowEmotion(emotionPosition, _appearanceDuration, _disappearanceDuration);
    }

    private void ShowEmotion(Vector2 position, float apperanceDuration, float disappearanceDuration)
    {
        _emotion.position = position;
        _emotion.localScale = Vector3.zero;
        _emotion.gameObject.SetActive(true);

        _emotion.DOShakeRotation(apperanceDuration);
        _emotion.DOMove(_endDistance, apperanceDuration).SetRelative();
        _emotion.DOScale(_baseEmotionScale, apperanceDuration)
                .OnComplete(() => _emotion.DOScale(Vector3.zero, disappearanceDuration)
                .OnComplete(() => _emotion.gameObject.SetActive(false)));
    }
}
