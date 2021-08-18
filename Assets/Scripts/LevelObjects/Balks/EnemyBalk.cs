using System.Collections.Generic;
using UnityEngine;

public class EnemyBalk : Balk
{
    [SerializeField] private BalkAINode _balkAINode;
    [SerializeField] private BalkMovement _balkMovement;

    public BalkMovement BalkMovement => _balkMovement;
    public int NearBalksCount => _balkAINode.NearBalksCount;

    public EnemyBalk GetRandomHigherBalk()
    {
        return _balkAINode.GetRandomHigherBalk();
    }

    public EnemyBalk GetRandomBalk()
    {
        return _balkAINode.GetRandomBalk();
    }

    public override void ScaleCamera(float dragValue)
    {
        return;
    }
}
