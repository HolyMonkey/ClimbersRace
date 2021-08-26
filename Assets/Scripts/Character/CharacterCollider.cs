using System.Collections;
using UnityEngine;

public abstract class CharacterCollider : MonoBehaviour
{
    [SerializeField] private float _timeToCanAttack = 0.5f;
    protected Character Character;

    private Coroutine _disableAttackJob;

    private bool _canAttack = true;

    private void OnEnable()
    {
        Character.BeingAttack += OnBeingAttack;
    }

    private void OnDisable()
    {
        Character.BeingAttack -= OnBeingAttack;
    }

    protected virtual void Awake()
    {
        Character = GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Coin coin))
        {
            CollectCoin(coin);
        }

        if (collider.TryGetComponent(out DeathCollider deathCollider))
            Character.Die();

        if (collider.TryGetComponent(out Trap trap))
        {
            if (!Character.IsAttachingBalk)
                Character.CollideWithTrap();
        }

        if (!Character.IsAttachingBalk && collider.TryGetComponent(out Balk balk))
        {
            if (balk.HasCharacter && _canAttack)
                Character.AttackEnemy(balk.CurrentCharacter, collider.ClosestPointOnBounds(balk.CurrentCharacter.transform.position));

            TrySetupBalkConnection(balk);
        }
    }

    //private void OnTriggetExit(Collider collider)
    //{
    //    if (_canAttack && !Character.IsAttachingBalk && collider.TryGetComponent(out Balk balk))
    //        TrySetupBalkConnection(balk);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (!Character.IsAttachingBalk && _canAttack)
        {
            if (collision.gameObject.TryGetComponent(out Character enemy))
            {
                if (enemy.IsAttachingBalk)
                {
                    Character.AttackEnemy(enemy, collision.contacts[0].point);
                }
            }
        }
    }

    protected void BalkConnection(Balk balk, Character character)
    {
        balk.Interaction(character);
        character.AttachToBalk(balk);
    }

    protected abstract void TrySetupBalkConnection(Balk balk);

    protected abstract void CollectCoin(Coin coin);


    private void OnBeingAttack()
    {
        if (_disableAttackJob != null)
        {
            StopCoroutine(_disableAttackJob);
            _disableAttackJob = null;
        }
        _disableAttackJob = StartCoroutine(DisableAttack(_timeToCanAttack));
    }

    private IEnumerator DisableAttack(float time)
    {
        _canAttack = false;

        yield return new WaitForSeconds(time);

        _canAttack = true;

        _disableAttackJob = null;
    }
}
