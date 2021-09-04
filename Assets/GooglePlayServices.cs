using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class GooglePlayServices : MonoBehaviour
{
    public Text txt;
    private bool signedIn;

    // Start is called before the first frame update
    void Start()
    {
        // authenticate user:
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            txt.text = result.ToString();
            signedIn = result == SignInStatus.Success;
        });
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => {
            txt.text = result.ToString();
            signedIn = result == SignInStatus.Success;
        });
    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
        signedIn = false;
    }

    public bool IsSignedIn()
    {
        return this.signedIn;
    }
}
