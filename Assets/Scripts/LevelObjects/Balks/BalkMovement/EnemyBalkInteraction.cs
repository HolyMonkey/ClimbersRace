using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBalkInteraction : BalkInteraction
{
    [SerializeField] private BalkAINode _balkAINode;

    public BalkAINode BalkAINode => _balkAINode;
}
