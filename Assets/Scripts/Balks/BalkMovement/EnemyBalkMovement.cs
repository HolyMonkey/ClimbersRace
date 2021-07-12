using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBalkMovement : MonoBehaviour
{
    [SerializeField] private BalkMovementHandler _balkMovement;
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private float _pullingForce;
    [SerializeField] private float _tolerance;

    private EnemyMovement _enemyMovement;
    private float _basePositionZ;

    private void Awake()
    {
        _basePositionZ = transform.position.z;
    }

    private void Update()
    {
        if(_enemyMovement != null)
        {
            Vector3 direction = (_enemyMovement.transform.position - _enemyMovement.NextTargetPosition).normalized;
            Vector3 newPosition = _enemyMovement.transform.position + direction * _pullingForce;
            newPosition.z = _basePositionZ;
            Debug.DrawLine(_enemyMovement.transform.position, _enemyMovement.NextTargetPosition, Color.green);

            _balkMovement.DragBalk(newPosition);
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
                _balkMovement.BeginDragBalk();

                _springJoint.tolerance = _tolerance;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyMovement enemyMovement))
        {
            _enemyMovement = null;
            _balkMovement.FinishDragBalk();

            _springJoint.tolerance = 0;
        }
    }
}
