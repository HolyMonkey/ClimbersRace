using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexGames;
using UnityEngine.UI;

public class Advertisement : MonoBehaviour
{
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

    public void OnShow()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show();
#endif
    }
}
