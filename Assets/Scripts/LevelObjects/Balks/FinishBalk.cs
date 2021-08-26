using UnityEngine;

public class FinishBalk : Balk
{
    [SerializeField] private Level _level;
    [SerializeField] private Collider _collider;
    [SerializeField] private BalkMovement _balkMovement;

    public override void Interaction(Character character)
    {
        base.Interaction(character);

        _collider.enabled = false;

        _level.StartBonusGame(this, character);
    }

    public void DragFinishBalk(float value)
    {
        _balkMovement.DragBalk(Vector3.down, value);
    }

    public void FinishPush(BonusWall targetWall, AnimationCurve YCurve)
    {
        ScaleCamera(0);

        CurrentCharacter.BonusPush(targetWall, YCurve);
    }
}
