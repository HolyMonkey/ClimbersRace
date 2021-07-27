using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWallCreator : MonoBehaviour
{
    [SerializeField] private Vector2Int _wallSize;
    [SerializeField] private int _playerStartTailIndex, _enemyStartTailIndex;
    [SerializeField] private GameWall _wall;
    [SerializeField] private GameWallTileContentFactory _contentFactory;

    private void Start()
    {
        _wall.Initialize(_wallSize, _contentFactory, _playerStartTailIndex, _enemyStartTailIndex);
    }
}
