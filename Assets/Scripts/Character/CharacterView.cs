using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField]private Character _character;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _character.AttachingBalk += OnAttachingBalk;
        _character.DetachingBalk += OnDetachingBalk;
    }

    private void OnDisable()
    {
        _character.AttachingBalk -= OnAttachingBalk;
        _character.DetachingBalk -= OnDetachingBalk;
        
    }

    private void Update()
    {
        Rotating();
    }

    private void OnAttachingBalk(Balk balk)
    {
        _animator.SetBool(CharacterAnimatorController.Params.Flying, false);
    }

    private void OnDetachingBalk()
    {
        _animator.SetBool(CharacterAnimatorController.Params.Flying, true);
    }

    private void Rotating()
    {

    }
}

