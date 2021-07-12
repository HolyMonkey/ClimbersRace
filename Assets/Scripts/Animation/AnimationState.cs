using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationState : MonoBehaviour
{
    [SerializeField] private MovementHandler _movement;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _movement.CatchedBalkOnRight += OnCatchedBalkOnRight;
        _movement.CatchedBalkOnLeft += OnCatchedBalkOnLeft;
        _movement.SlidingDown += OnSlidingDown;
    }

    private void OnDisable()
    {
        _movement.CatchedBalkOnRight -= OnCatchedBalkOnRight;
        _movement.CatchedBalkOnLeft -= OnCatchedBalkOnLeft;
        _movement.SlidingDown -= OnSlidingDown;
    }

    private void OnCatchedBalkOnRight()
    {
        _animator.SetBool(SampleAnimationController.Params.RightHang, true);
        _animator.SetBool(SampleAnimationController.Params.LeftHang, false);
    }

    private void OnCatchedBalkOnLeft()
    {
        _animator.SetBool(SampleAnimationController.Params.RightHang, false);
        _animator.SetBool(SampleAnimationController.Params.LeftHang, true);
    }

    private void OnSlidingDown()
    {
        _animator.Play(SampleAnimationController.States.MissBalk);
    }
}
