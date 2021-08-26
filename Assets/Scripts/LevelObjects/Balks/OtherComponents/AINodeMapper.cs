using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINodeMapper : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 4.5f;
    [SerializeField] private List<BalkAINode> _allNodes;

#if UNITY_EDITOR
    [ContextMenu("MapNodes(NO UNDO)")]
    private void MapNodes()
    {
        foreach (BalkAINode node in _allNodes)
            node.ClearNodes();

        for (int i = 0; i < _allNodes.Count; i++)
        {
            for (int j = i + 1; j < _allNodes.Count; j++)
            {
                if (Vector3.Distance(_allNodes[i].BalkPosition, _allNodes[j].BalkPosition) < _maxDistance)
                {
                    _allNodes[i].AddNearNode(_allNodes[j]);
                    _allNodes[j].AddNearNode(_allNodes[i]);
                    UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(_allNodes[i]);
                    UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(_allNodes[j]);
                }
            }

            _allNodes[i].Validate();
        }

        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }
#endif
}
