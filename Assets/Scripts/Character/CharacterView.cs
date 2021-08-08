using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private float _rotationSpeed;

    [Header("IK")]
    [SerializeField] [Range(0f, 1f)] private float _rightHandWeight;
    [SerializeField] [Range(0f, 1f)] private float _leftHandWeight;

    private Animator _animator;

    private Vector3 _targetLookVector;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _character.AttachingBalk += OnAttachingBalk;
        _character.DetachingBalk += OnDetachingBalk;
        _character.Falling += OnFalling;
    }

    private void OnDisable()
    {
        _character.AttachingBalk -= OnAttachingBalk;
        _character.DetachingBalk -= OnDetachingBalk;
        _character.Falling -= OnFalling;
    }

    private void Update()
    {
        if (_character.IsAttachingBalk)
            LookAt(_character._currentBalk.LookAtPoint);
        else
            LookAt(_character.Velocity);
    }

    private void OnAttachingBalk(Balk balk)
    {
        _animator.SetBool(IKCharacterAnimatorController.Params.Flying, false);
        _animator.SetBool(IKCharacterAnimatorController.Params.Falling, false);
    }

    private void OnDetachingBalk()
    {
        _animator.SetBool(IKCharacterAnimatorController.Params.Flying, true);
    }

    private void OnFalling()
    {
        _animator.SetBool(IKCharacterAnimatorController.Params.Falling, true);
    }

    private void LookAt(Vector3 targetPoint)
    {
        targetPoint.y = _character.transform.position.y;
        transform.LookAt(targetPoint);
    }
}

