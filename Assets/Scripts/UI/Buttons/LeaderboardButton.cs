using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using AnimatedUI;

public class LeaderboardButton : MonoBehaviour
{
    [SerializeField] private Advertisement _leaderboard;
    [SerializeField] private CanvasFade _leaderboardScreenFade;
    [SerializeField] private CanvasFade _logInScreenFade;

    public void TryOpenLeaderboard()
    {
        if (PlayerAccount.IsAuthorized)
        {
            _leaderboard.GetLeaderboardEntries();
            _leaderboardScreenFade.Show();
        }
        else
        {
            _logInScreenFade.Show();
        }
    }
}
