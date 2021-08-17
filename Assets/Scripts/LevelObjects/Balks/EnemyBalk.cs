using System.Collections.Generic;
using UnityEngine;

public class EnemyBalk : Balk
{
    [SerializeField] private List<EnemyBalk> _nearBalks;
    [SerializeField] private BalkMovement _balkMovement;

    public BalkMovement BalkMovement => _balkMovement;

    private List<EnemyBalk> _higherBalks = new List<EnemyBalk>();

    private void OnValidate()
    {
        for (int i = 0; i < _nearBalks.Count; i++)
        {
            if (transform.position.y < _nearBalks[i].transform.position.y && !_higherBalks.Contains(_nearBalks[i]))
                _higherBalks.Add(_nearBalks[i]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < _nearBalks.Count; i++)
        {
            Vector3 targetPos = (_nearBalks[i].transform.position + transform.position) / 2;
            Gizmos.DrawLine(transform.position, targetPos);
        }
    }

    public EnemyBalk GetRandomHigherBalk()
    {
        int randomValue = Random.Range(0, _higherBalks.Count);
        return _higherBalks[randomValue];
    }

    public override void ScaleCamera(float dragValue)
    {
        
    }
}
