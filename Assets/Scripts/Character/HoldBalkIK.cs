using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HoldBalkIK : MonoBehaviour
{
    [SerializeField] private bool _ikActive;
    [SerializeField] [Range(0f, 1f)] private float _rightHandWeight;
    [SerializeField] [Range(0f, 1f)] private float _leftHandWeight;
    [SerializeField] private Character _character;

    private Animator _animator;
    private Transform _targetForRightHand;
    private Transform _targetForLeftHand;

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

    private void OnAnimatorIK()
    {
        if (_animator && _ikActive)
        {
            if (_targetForRightHand != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandWeight);
                _animator.SetIKPosition(AvatarIKGoal.RightHand, _targetForRightHand.position);
            }

            if (_targetForLeftHand != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftHandWeight);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, _targetForLeftHand.position);
            }
        }
    }

    private void SetTarget(Transform targetForRightHand, Transform targetForLeftHand)
    {
        _targetForRightHand = targetForRightHand;
        _targetForLeftHand = targetForLeftHand;
    }

    private void OnAttachingBalk(Balk balk)
    {
        if (Vector3.Angle(_character.PushVector, balk.transform.position) > 90f)
            SetTarget(balk.NearPoint, balk.FarPoint);
        else
            SetTarget(balk.FarPoint, balk.NearPoint);
    }

    private void OnDetachingBalk()
    {
        SetTarget(null, null);
    }
}
