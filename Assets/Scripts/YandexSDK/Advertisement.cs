using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agava.YandexGames;

namespace YandexGames.Samples
{
    public class Advertisement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerScore;
        [SerializeField] private TMP_Text[] _leaderNames;
        [SerializeField] private TMP_Text[] _scoreList;
        [SerializeField] private string _leaderboardName = "LeaderBoard";

        private Money _money;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
            //WebBackgroundMute.Enabled = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            yield return YandexGamesSdk.WaitForInitialization();

            OnRequestPersonalProfileDataPermissionButtonClick();

            if (PlayerAccount.IsAuthorized == false)
            {
                OnAuthorize();
            }
                
        }

        public void OnShowInterestial()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            InterestialAd.Show();
#endif
        }

        public void OnShowVideo()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            VideoAd.Show();
#endif
        }

        public void OnAuthorize()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.Authorize();
#endif
        }

        public void OnRequestPersonalProfileDataPermissionButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.RequestPersonalProfileDataPermission();
#endif
        }

        public void OnSetLeaderboardScoreButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.SetScore(_leaderboardName, _money.CurrentMoney);
#endif
        }

        public void OnGetLeaderboardEntriesButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.GetEntries(_leaderboardName, (result) =>
            {
                int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;
                for (int i = 0; i < leadersNumber; i++)
                {
                    string name = result.entries[i].player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = "Анонимно";

                    _leaderNames[i].text = name;
                    _scoreList[i].text = result.entries[i].formattedScore;
                }
            });
#endif
        }
    }
}
