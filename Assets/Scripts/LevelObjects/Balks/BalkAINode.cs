using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkAINode : MonoBehaviour
{
    [SerializeField] private BalkMovement _balkMovement;
    [SerializeField] private List<BalkAINode> _nearNodes;
    [SerializeField] private List<BalkAINode> _higherNodes;

    public BalkMovement BalkMovement => _balkMovement;
    public Vector3 BalkPosition => _balkMovement.transform.position;
    public int NearNodesCount => _nearNodes.Count;
    public int HigherNodesCount => _higherNodes.Count;

    private void OnValidate()
    {
        Validate();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _nearNodes.Count; i++)
        {
            if (Vector3.Distance(BalkPosition, _nearNodes[i].BalkPosition) > 4f) //~max distance for correct enemyMovement
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.blue;

            Vector3 targetPos = (_nearNodes[i].BalkPosition + BalkPosition) / 2;
            Gizmos.DrawLine(BalkPosition, targetPos);
        }
    }

    public BalkAINode GetRandomHigherNode()
    {
        return GetNodeFromList(_higherNodes);
    }

    public BalkAINode GetRandomNode()
    {
        return GetNodeFromList(_nearNodes);
    }

    public void ClearNodes()
    {
        _nearNodes.Clear();
        _higherNodes.Clear();
    }

    public void AddNearNode(BalkAINode node)
    {
        _nearNodes.Add(node);
    }

    public void Validate()
    {
        for (int i = 0; i < _nearNodes.Count; i++)
        {
            if (BalkPosition.y < _nearNodes[i].BalkPosition.y && !_higherNodes.Contains(_nearNodes[i]))
                _higherNodes.Add(_nearNodes[i]);
        }

        foreach (BalkAINode node in _higherNodes)
        {
            if (_higherNodes.Contains(this) || node.BalkPosition.y < BalkPosition.y || !_nearNodes.Contains(node))
                _higherNodes.Remove(node);
        }

        foreach (BalkAINode node in _nearNodes)
        {
            if (_nearNodes.Contains(this))
                _nearNodes.Remove(this);
        }
    }

    private BalkAINode GetNodeFromList(List<BalkAINode> nodes)
    {
        if (nodes.Count > 0)
        {
            int randomValue = UnityEngine.Random.Range(0, nodes.Count);
            return nodes[randomValue];
        }
        else return null;
    }
}
