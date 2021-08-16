using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private Transform _finishWall;

    public void RotateFinishWall(Vector3 targetLookAt)
    {
        if (_finishWall)
        {
            Vector3 direction = _finishWall.position - targetLookAt;
            direction.y = 0;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _finishWall.rotation = lookRotation;
        }
    }
}
