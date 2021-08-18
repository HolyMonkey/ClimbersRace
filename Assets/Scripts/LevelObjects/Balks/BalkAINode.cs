using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkAINode : MonoBehaviour
{
    [SerializeField] private EnemyBalk _balk;
    [SerializeField] private List<BalkAINode> _nearBalks;
    [SerializeField] private List<BalkAINode> _higherBalks;

    public EnemyBalk Balk => _balk;
    public Vector3 BalkPosition => _balk.transform.position;
    public int NearBalksCount => _nearBalks.Count;

    private void OnValidate()
    {
        for (int i = 0; i < _nearBalks.Count; i++)
        {
            if (BalkPosition.y < _nearBalks[i].BalkPosition.y && !_higherBalks.Contains(_nearBalks[i]))
                _higherBalks.Add(_nearBalks[i]);
        }

        foreach (BalkAINode node in _higherBalks)
        {
            if (_higherBalks.Contains(this) || node.BalkPosition.y < BalkPosition.y)
                _higherBalks.Remove(node);
        }

        foreach (BalkAINode node in _nearBalks)
        {
            if (_nearBalks.Contains(this))
                _nearBalks.Remove(this);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _nearBalks.Count; i++)
        {
            if (Vector3.Distance(BalkPosition, _nearBalks[i].BalkPosition) > 4f) //~max distance for correct enemyMovement
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.blue;

            Vector3 targetPos = (_nearBalks[i].BalkPosition + BalkPosition) / 2;
            Gizmos.DrawLine(BalkPosition, targetPos);
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

    public void AddNearNode(BalkAINode node)
    {
        _nearBalks.Add(node);
    }

    private EnemyBalk GetBalkFromList(List<BalkAINode> balks)
    {
        if (balks.Count > 0)
        {
            int randomValue = Random.Range(0, balks.Count);
            return balks[randomValue].Balk;
        }
        else return null;
    }
}
