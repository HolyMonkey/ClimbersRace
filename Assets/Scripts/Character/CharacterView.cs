using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private Character _character;

    [Header("IK")]
    [SerializeField] private bool _ikActive;
    [SerializeField] [Range(0f, 1f)] private float _rightHandWeight;
    [SerializeField] [Range(0f, 1f)] private float _leftHandWeight;

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
        {
            LookAt(_character.CurrentBalk.LookAtPoint);

            UpdateIK(_character.CurrentBalk);
        }
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

        SetIKTarget(null, null);
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

    private void UpdateIK(Balk balk)
    {
        if (Vector3.Dot(_character.CurrentBalk.LookAtPoint,transform.right) > 0.1f)
            SetIKTarget(balk.NearPoint, balk.FarPoint);
        else
            SetIKTarget(balk.FarPoint, balk.NearPoint);
    }

    private void SetIKTarget(Transform targetForRightHand, Transform targetForLeftHand)
    {
        _targetForRightHand = targetForRightHand;
        _targetForLeftHand = targetForLeftHand;
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
}