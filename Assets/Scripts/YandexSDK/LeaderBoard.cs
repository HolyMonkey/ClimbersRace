using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexGames;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private GameObject _entryPanel;

#if UNITY_WEBGL && !UNITY_EDITOR
    private void Start()
    {
        Time.timeScale = 0;

        if (PlayerAccount.IsAuthorized)
        {
            Time.timeScale = 1;
            _entryPanel.SetActive(false);
            Leaderboard.SetScore("PlaytestBoard", Random.Range(1, 100));
        }
        else if (PlayerAccount.IsAuthorized == false)
        {
            _entryPanel.SetActive(true);
        }
    }

    public void EntryToAccount()
    {
        PlayerAccount.Authenticate(true);
        _entryPanel.SetActive(false);

        Leaderboard.SetScore("PlaytestBoard", Random.Range(1, 100));

        Time.timeScale = 1;
    }

    public void OnClickNotEntryButton()
    {
        _entryPanel.SetActive(false);
        PlayerAccount.Authenticate(false);
        Time.timeScale = 1;
    }
#endif
}