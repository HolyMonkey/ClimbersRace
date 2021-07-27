using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FlyingAnimationState : MonoBehaviour
{
    [SerializeField] private CharacterInteractionHandler _character;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _character.AttachingBalk += OnAttachingBalk;
        _character.DetachingBalk += OnDetachingBalk;
        _character.SlidingDown += OnSlidingDown;
    }

    private void OnDisable()
    {
        _character.AttachingBalk -= OnAttachingBalk;
        _character.DetachingBalk -= OnDetachingBalk;
        _character.SlidingDown -= OnSlidingDown;
    }

    private void Update()
    {
        if (_character.IsAttachingBalk)
            _animator.SetFloat(FlyingSampleAnimationController.Params.DistanceToBalk, _character.DistanceToBalk);
    }

    private void OnAttachingBalk(Balk balk)
    {
        _animator.SetBool(FlyingSampleAnimationController.Params.Flying, false);    
    }

    private void OnDetachingBalk()
    {
        _animator.SetBool(FlyingSampleAnimationController.Params.Flying, true);
    }

    private void OnSlidingDown()
    {
        _animator.Play(FlyingSampleAnimationController.States.MissBalk);
    }
}
