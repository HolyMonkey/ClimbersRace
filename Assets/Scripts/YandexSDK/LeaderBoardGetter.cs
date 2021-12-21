using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexGames.Utility;

namespace YandexGames.Samples
{
    public class LeaderBoardGetter : MonoBehaviour
    {
        public void OnGetProfileData()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.GetProfileData((result) =>
            {
                string name = result.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Анонимно";
            });
#endif
        }
    }
}
