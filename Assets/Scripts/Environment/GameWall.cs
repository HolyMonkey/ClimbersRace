using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameWall : MonoBehaviour
{
    [SerializeField] private Transform _wall;
    [SerializeField] private GameWallTile _tilePrefab;

    private Vector2Int _size;

    private GameWallTile[] _tiles;
    private GameWallTileContentFactory _contentFactory;

    private List<GameWallTile> _spawnedBalks = new List<GameWallTile>();

    public int SpawnedBalksCount => _spawnedBalks.Count;

    public void Initialize(Vector2Int size, GameWallTileContentFactory contentFactory, int playerStartTailIndex, int enemyStartTailIndex)
    {
        _size = size;
        _wall.localScale = new Vector3(size.x, size.y, 1f);

        Vector2 offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);

        _tiles = new GameWallTile[size.x * size.y];
        _contentFactory = contentFactory;

        for (int i = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                GameWallTile tile = _tiles[i] = Instantiate(_tilePrefab);
                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, y, 0f);

                tile.Content = _contentFactory.Get(GameWallTileContentType.Wall);
            }
        }
        ToggleBalk(_tiles, 7, GameWallTileContentType.PlayerBalk, playerStartTailIndex);
        ToggleBalk(_tiles, 4, GameWallTileContentType.EnemyBalk, enemyStartTailIndex);
    }

    public void ToggleBalk(GameWallTile[] tiles, int spread, GameWallTileContentType content, int startPlaceIndex)
    {
        for (int i = startPlaceIndex; i < tiles.Length; i++)
        {
            if (i == startPlaceIndex || i % spread == 0)
            {
                if (tiles[i].Content.Type == GameWallTileContentType.Wall)
                {
                    tiles[i].Content = _contentFactory.Get(content);
                    _spawnedBalks.Add(tiles[i]);
                }
            }
        }
    }

    public List<GameWallTile> GetPlayerBalksOnWall()
    {
        var enemyBalks = _spawnedBalks.Where(balk => balk.Content.Type.Equals(GameWallTileContentType.PlayerBalk)).ToList();

        return enemyBalks;
    }
}

