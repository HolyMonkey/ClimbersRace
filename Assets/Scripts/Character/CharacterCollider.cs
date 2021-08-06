using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class CharacterCollider : MonoBehaviour
{
    [SerializeField] private Character _character;
    //[SerializeField]private float _timeToLeaveBalk = 0.3f;

    private Collider _collider;

    public event UnityAction<Vector3> KnockedDownEnemy;

    //private void OnEnable()
    //{
    //    _character.DetachingBalk += OnDetachingBalk;
    //}

    //private void OnDisable()
    //{
    //    _character.DetachingBalk -= OnDetachingBalk;

    //}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Character enemy))
        {
            if (enemy.IsAttachingBalk)
            {
                KnockedDownEnemy?.Invoke(transform.position);
            }
        }

        if (CheckBalkType(out Balk balk, collider))
        {
            _character.AttachToBalk(balk);
        }
    }

    protected abstract bool CheckBalkType(out Balk balk, Collider collider);

    //private void OnDetachingBalk()
    //{
    //    StartCoroutine(DisableColliderOnTime(_timeToLeaveBalk));
    //}

    //private IEnumerator DisableColliderOnTime(float delay)
    //{
    //    _collider.enabled = false;

    //    yield return new WaitForSeconds(delay);

    //    _collider.enabled = true;
    //}
}
