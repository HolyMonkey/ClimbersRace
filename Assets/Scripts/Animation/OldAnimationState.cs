using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OldAnimationState : MonoBehaviour
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
        _movement.Flying += OnFlying;
        _movement.SlidingDown += OnSlidingDown;
    }

    private void OnDisable()
    {
        _movement.CatchedBalkOnRight -= OnCatchedBalkOnRight;
        _movement.CatchedBalkOnLeft -= OnCatchedBalkOnLeft;
        _movement.Flying -= OnFlying;
        _movement.SlidingDown -= OnSlidingDown;
    }

    private void OnCatchedBalkOnRight()
    {
        _animator.SetBool(OldSampleAnimationController.Params.RightHang, true);
        _animator.SetBool(OldSampleAnimationController.Params.LeftHang, false);
        _animator.SetBool(OldSampleAnimationController.Params.Flying, false);
    }

    private void OnCatchedBalkOnLeft()
    {
        _animator.SetBool(OldSampleAnimationController.Params.RightHang, false);
        _animator.SetBool(OldSampleAnimationController.Params.LeftHang, true);
        _animator.SetBool(OldSampleAnimationController.Params.Flying, false);
    }

    private void OnFlying()
    {
        _animator.SetBool(OldSampleAnimationController.Params.RightHang, false);
        _animator.SetBool(OldSampleAnimationController.Params.LeftHang, false);
        _animator.SetBool(OldSampleAnimationController.Params.Flying, true);
    }

    private void OnSlidingDown()
    {
        _animator.Play(OldSampleAnimationController.States.MissBalk);
    }
}
