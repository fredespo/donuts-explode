using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class GooglePlayServices : MonoBehaviour
{
    public UnityEvent afterInitialSignInAttempt;
    private bool signedIn;

    // Start is called before the first frame update
    void Start()
    {
        // authenticate user:
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            afterInitialSignInAttempt.Invoke();
            signedIn = result == SignInStatus.Success;
        });
        PlayGamesPlatform.Activate();
    }

    public void ShowLeaderboard()
    {
        if(!this.IsSignedIn())
        {
            this.SignIn();
        }
        Social.ShowLeaderboardUI();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => {
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
