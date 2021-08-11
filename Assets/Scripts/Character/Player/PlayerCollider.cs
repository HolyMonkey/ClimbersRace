using UnityEngine;

public class PlayerCollider : CharacterCollider
{
    protected override bool CheckBalkType(out Balk balk, Collider collider)
    {
        if (collider.TryGetComponent(out Balk playerBalk))
        {
            if (playerBalk is PlayerBalk || playerBalk is FinishBalk)
            {
                balk = playerBalk;
                return true;
            }
        }

        balk = null;
        return false;
    }
}

