using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterCollider : MonoBehaviour
{
    protected Character Character;
    //[SerializeField]private float _timeToLeaveBalk = 0.1f;

    //private Collider _collider;

    public event UnityAction<Vector3> KnockedDownEnemy;

    protected virtual void Awake()
    {
        Character = GetComponent<Character>();
    }

    //private void OnEnable()
    //{
    //    _character.DetachingBalk += OnDetachingBalk;
    //}

    //private void OnDisable()
    //{
    //    _character.DetachingBalk -= OnDetachingBalk;

    //}

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Character enemy))
        {
            if (enemy.IsAttachingBalk)
            {
                KnockedDownEnemy?.Invoke(transform.position);
            }
        }

        if (collider.TryGetComponent(out Trap trap))
        {
            if (!Character.IsAttachingBalk)
                Character.CollideWithTrap();
        }

        TrySetupBalkConnection(collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out DeathCollider deathCollider))
        {
            Character.Die();
        }
    }

    protected abstract void TrySetupBalkConnection(Collider collider);

    protected void SetupConnection(Balk balk, Character character)
    {
        balk.Interaction(character);
        character.AttachToBalk(balk);
    }

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
