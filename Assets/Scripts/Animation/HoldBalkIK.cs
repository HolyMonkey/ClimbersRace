using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HoldBalkIK : MonoBehaviour
{
    [SerializeField] private bool _ikActive;
    [SerializeField] [Range(0f, 1f)] private float _rightHandWeight;
    [SerializeField] [Range(0f, 1f)] private float _leftHandWeight;

    private Animator _animator;
    private Transform _targetForRightHand;
    private Transform _targetForLeftHand;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (_animator)
        {
            if (_ikActive)
            {
                if(_targetForRightHand != null)
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
    }

    public void SetTarget(Transform targetForRightHand, Transform targetForLeftHand)
    {
        _targetForRightHand = targetForRightHand;
        _targetForLeftHand = targetForLeftHand;
    }
}
