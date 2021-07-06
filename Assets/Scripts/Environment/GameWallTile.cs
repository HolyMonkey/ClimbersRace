using UnityEngine;

public class GameWallTile : MonoBehaviour
{
    private GameWallTileContent _content;

    public GameWallTileContent Content
    {
        get => _content;
        set
        {
            if (_content != null)
                _content.Recycle();

            _content = value;
            _content.transform.localPosition = transform.localPosition;
        }
    }
}

