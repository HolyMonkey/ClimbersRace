using UnityEngine;
public class PlayerCollider : CharacterCollider
{
    protected override bool CheckBalkType(out Balk balk, Collider collider)
    {
        if (collider.TryGetComponent(out PlayerBalk playerBalk))
        {
            balk = playerBalk;
            return true;
        }
        else
        {
            balk = null;
            return false;
        }
    }
}

