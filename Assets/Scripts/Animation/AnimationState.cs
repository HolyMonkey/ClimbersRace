using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationState : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private Animator _animator;

    private string _rightHangBoolName = "RightHang", _leftHangBoolName = "LeftHang", _flyBoolName = "Flying", _slideDownAnimation = "Miss Balk";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _movement.CatchedtBalkOnRight += OnRightHangState;
        _movement.CatchedtBalkOnLeft += OnLeftHangState;
        _movement.LeftBalk += OnFlyState;
        _movement.SlidDown += OnSlidDown;
    }

    private void OnDisable()
    {
        _movement.CatchedtBalkOnRight -= OnRightHangState;
        _movement.CatchedtBalkOnLeft -= OnLeftHangState;
        _movement.LeftBalk -= OnFlyState;
        _movement.SlidDown -= OnSlidDown;
    }

    private void OnRightHangState()
    {
        _animator.SetBool(_rightHangBoolName, true);
        _animator.SetBool(_leftHangBoolName, false);
        _animator.SetBool(_flyBoolName, false);
    }

    private void OnLeftHangState()
    {
        _animator.SetBool(_rightHangBoolName, false);
        _animator.SetBool(_leftHangBoolName, true);
        _animator.SetBool(_flyBoolName, false);
    }

    private void OnFlyState()
    {
        _animator.SetBool(_rightHangBoolName, false);
        _animator.SetBool(_leftHangBoolName, false);
        _animator.SetBool(_flyBoolName, true);
    }

    private void OnSlidDown()
    {
        _animator.Play(_slideDownAnimation);
    }
}
