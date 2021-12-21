using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YandexGames.Utility;

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
            Leaderboard.SetScore("LeaderBoard", _money.CurrentMoney);
#endif
        }

        public void OnGetLeaderboardEntriesButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.GetEntries("LeaderBoard", (result) =>
            {
                for (int i = 0; i < _leaderNames.Length; i++)
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
