using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINodeAutoSetter : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 4f;
    [SerializeField] private List<BalkAINode> _allNodes;

    [ContextMenu("ConfigureBalks")]
    private void ConfigureBalks()
    {
        for (int i = 0; i < _allNodes.Count; i++)
        {
            for (int j = i + 1; j < _allNodes.Count; j++)
            {
                if (Vector3.Distance(_allNodes[i].BalkPosition, _allNodes[j].BalkPosition) < _maxDistance)
                {
                    _allNodes[i].AddNearNode(_allNodes[j]);
                }
            }
        }
    }
}
