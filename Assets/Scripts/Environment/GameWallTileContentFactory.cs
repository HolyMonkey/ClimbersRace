using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameWallTileContentFactory : GameObjectFactory
{
    [SerializeField] private GameWallTileContent _emptyPrefab;
    [SerializeField] private GameWallTileContent _playerBalkPrefab;
    [SerializeField] private GameWallTileContent _enemyBalkPrefab;

    public void Reclaim(GameWallTileContent content)
    {
        Destroy(content.gameObject);
    }

    public GameWallTileContent Get(GameWallTileContentType type)
    {
        switch (type)
        {
            case GameWallTileContentType.Wall:
                return Get(_emptyPrefab);
            case GameWallTileContentType.PlayerBalk:
                return Get(_playerBalkPrefab);
            case GameWallTileContentType.EnemyBalk:
                return Get(_enemyBalkPrefab);
        }

        return null;
    }

    private GameWallTileContent Get(GameWallTileContent prefab)
    {
        GameWallTileContent instance = CreateGameObjectInstance(prefab);
        instance.OriginFactory = this;
        return instance;
    }
}
