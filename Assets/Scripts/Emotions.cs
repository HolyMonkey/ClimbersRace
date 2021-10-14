using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Emotions : MonoBehaviour
{
    [SerializeField] private GameObject _emotionPrefab;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Character _player;
    [SerializeField] private float _appearanceDuration;
    [SerializeField] private float _disappearanceDuration;
    [SerializeField] private Vector2 _endDistance;
    [SerializeField] private Vector2 _offset;

    private Vector3 _baseEmotionScale;

    private void Awake()
    {
        _baseEmotionScale = _emotionPrefab.transform.localScale;
    }

    private void OnEnable()
    {
        _player.AttackedEnemy += OnKnockedDownEnemy;
    }

    private void OnDisable()
    {
        _player.AttackedEnemy -= OnKnockedDownEnemy;
    }

    private void OnKnockedDownEnemy(Vector3 position)
    {
        //PositionConverter converter = new PositionConverter();

        Vector3 offset = new Vector3(_offset.x, _offset.y);
        //Vector2 emotionPosition = converter.ConvertWorldPositionToCanvasPosition(_canvas, position + offset);

        //ShowEmotion(emotionPosition, _appearanceDuration, _disappearanceDuration);
    }

    private void ShowEmotion(Vector2 position, float apperanceDuration, float disappearanceDuration)
    {
        Transform emotion = Instantiate(_emotionPrefab, position, Quaternion.identity, _canvas.transform).transform;
        emotion.localScale = Vector3.zero;
        emotion.gameObject.SetActive(true);

        emotion.DOShakeRotation(apperanceDuration);
        emotion.DOMove(_endDistance, apperanceDuration).SetRelative();
        emotion.DOScale(_baseEmotionScale, apperanceDuration)
                .OnComplete(() => emotion.DOScale(Vector3.zero, disappearanceDuration)
                .OnComplete(() => emotion.gameObject.SetActive(false)));
    }
}
