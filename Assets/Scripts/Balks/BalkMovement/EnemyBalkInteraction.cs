using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBalkInteraction : MonoBehaviour
{
    [SerializeField] private BalkInteractionHandler _balkInteraction;
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private float _pullingForce;
    [SerializeField] private float _tolerance;

    private EnemyMovement _enemyMovement;
    private float _basePositionZ;
    private float _baseTolerance;

    private void Awake()
    {
        _basePositionZ = transform.position.z;
        _baseTolerance = _springJoint.tolerance;
    }

    private void Update()
    {
        if(_enemyMovement != null)
        {
            Vector3 direction = (_enemyMovement.transform.position - _enemyMovement.NextTargetPosition).normalized;
            Vector3 newPosition = _enemyMovement.transform.position + direction * _pullingForce;
            newPosition.z = _basePositionZ;
            Debug.DrawLine(_enemyMovement.transform.position, _enemyMovement.NextTargetPosition, Color.green);

            _balkInteraction.DragBalk(newPosition);
            _enemyMovement.MoveToNextPosition();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(_enemyMovement == null)
        {
            if (other.gameObject.TryGetComponent(out EnemyMovement enemyMovement))
            {
                _enemyMovement = enemyMovement;
                _balkInteraction.BeginDragBalk();

                _springJoint.tolerance = _tolerance;
            }
        }
        else
        {
            if (other.gameObject.TryGetComponent(out CharacterCollider playerMovement))
            {
                _enemyMovement = null;
                _springJoint.tolerance = _baseTolerance;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyMovement enemyMovement))
        {
            _enemyMovement = null;
            _balkInteraction.FinishDragBalk();

            _springJoint.tolerance = _baseTolerance;
        }
    }
}
