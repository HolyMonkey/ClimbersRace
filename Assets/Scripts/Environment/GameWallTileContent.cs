using UnityEngine;

public class GameWallTileContent : MonoBehaviour
{
    [SerializeField] private GameWallTileContentType _type;

    public GameWallTileContentType Type => _type;
    public GameWallTileContentFactory OriginFactory { get; set; }

    public void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}

public enum GameWallTileContentType
{
    Wall,
    PlayerBalk,
    EnemyBalk
}

