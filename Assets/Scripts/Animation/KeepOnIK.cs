using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KeepOnIK : MonoBehaviour
{
    [SerializeField] private bool _ikActive = false;
    [SerializeField] private Transform _keepOnTarget = null;
    private Animator _animator;

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
                if(_keepOnTarget != null)
                {
                    float reach = _animator.GetFloat("RightHandReach");
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, reach);
                    _animator.SetIKPosition(AvatarIKGoal.RightHand, _keepOnTarget.position);
                }
            }
        } 
    }
}
