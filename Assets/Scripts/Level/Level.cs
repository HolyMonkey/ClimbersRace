using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private BonusGame _bonusGame;

    public void StartBonusGame(FinishBalk finishBalk)
    {
        _bonusGame.StartBonusGame(finishBalk);
    }
}