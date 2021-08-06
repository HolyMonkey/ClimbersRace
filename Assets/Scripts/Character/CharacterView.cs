using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private float _rotationSpeed;

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
            LookRotation(_character.PushVector);
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

    private void LookRotation(Vector3 direction)
    {
        direction.y = 0;

        Debug.Log("rotationDirection - " + direction);

        Quaternion lookRotation = Quaternion.LookRotation(-direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }
}

