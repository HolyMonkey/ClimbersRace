using System.Collections.Generic;
using UnityEngine;

public class EnemyBalk : Balk
{
    [SerializeField] private List<EnemyBalk> _nearBalks;
    [SerializeField] private BalkMovement _balkMovement;

    public BalkMovement BalkMovement => _balkMovement;
    public int NearBalksCount => _nearBalks.Count;

    [SerializeField]
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
        for (int i = 0; i < _nearBalks.Count; i++)
        {
            if (Vector3.Distance(transform.position, _nearBalks[i].transform.position) > 4f) //~max distance for correct enemyMovement
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.blue;

            Vector3 targetPos = (_nearBalks[i].transform.position + transform.position) / 2;
            Gizmos.DrawLine(transform.position, targetPos);
        }
    }

    public EnemyBalk GetRandomHigherBalk()
    {
        return GetBalkFromList(_higherBalks);
    }

    public EnemyBalk GetRandomBalk()
    {
        return GetBalkFromList(_nearBalks);
    }

    public override void ScaleCamera(float dragValue)
    {
        return;
    }

    private EnemyBalk GetBalkFromList(List<EnemyBalk> _balks)
    {
        if (_balks.Count > 0)
        {
            int randomValue = Random.Range(0, _balks.Count);
            return _balks[randomValue];
        }
        else return null;
    }
}
