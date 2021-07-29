using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationState : MonoBehaviour
{
    [SerializeField] private CharacterInteraction _character;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _character.AttachingBalk += OnAttachingBalk;
        _character.SlidingDown += OnSlidingDown;
    }

    private void OnDisable()
    {
        _character.AttachingBalk -= OnAttachingBalk;
        _character.SlidingDown -= OnSlidingDown;
    }

    private void Update()
    {
        if (_character.IsAttachingBalk)
            _animator.SetFloat(SampleAnimationController.Params.DistanceToBalk, _character.DistanceToBalk);
    }

    private void OnAttachingBalk(Balk balk)
    {
        _animator.SetBool(SampleAnimationController.Params.SlidingDown, false);
    }

    private void OnSlidingDown()
    {
        _animator.SetBool(SampleAnimationController.Params.SlidingDown, true);
        _animator.Play(SampleAnimationController.States.MissBalk);
    }
}
