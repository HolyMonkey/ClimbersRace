using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YandexGames.Samples
{
    public class Advertisement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerScore;
        [SerializeField] private TMP_Text[] _leaderNames;
        [SerializeField] private TMP_Text[] _scoreList;

        private Money _money;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
            _playerScore.text = "";
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            yield return YandexGamesSdk.WaitForInitialization();

            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.Authenticate(false);
            }
        }

        public void OnShowInterestialButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            InterestialAd.Show();
#endif
        }

        public void OnShowVideoButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show();
#endif
        }

        public void OnAuthenticateButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.Authenticate(false);
#endif
        }

        public void OnAuthenticateWithPermissionsButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.Authenticate(true);
#endif
        }

        public void OnSetLeaderboardScoreButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.SetScore("LeaderBoard", _money.CurrentMoney);
#endif
        }

        public void OnGetLeaderboardEntriesButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.GetEntries("LeaderBoard", (result) =>
            {
                //foreach (var entry in result.entries)
                //{
                //    _playerScore.text = ($"{entry.player.publicName} \t\t {entry.score}");
                //}
                for (int i = 0; i < result.entries.Length; i++)
                {
                    _leaderNames[i].text = result.entries[i].player.publicName;
                    _scoreList[i].text = result.entries[i].formattedScore;
                }
            });
#endif
        }
    }
}
