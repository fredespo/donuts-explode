﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using System;

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
        EnsureSignedIn(() =>
        {
            if(this.signedIn)
            {
                Social.ShowLeaderboardUI();
            }
        });
    }

    private void EnsureSignedIn(Action andThen)
    {
        if (!this.IsSignedIn())
        {
            this.SignIn(andThen);
        }
        else
        {
            andThen.Invoke();
        }
    }

    public void PostHighScoreAndThen(int score, Action<bool> action)
    {
        EnsureSignedIn(() =>
        {
            if(this.signedIn)
            {
                Social.ReportScore(score, "CgkIx6OD85cGEAIQAQ", (bool success) => {
                    action.Invoke(success);
                });
            }
            else
            {
                action.Invoke(false);
            }
        });
    }

    public void SignIn(Action andThen)
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => {
            signedIn = result == SignInStatus.Success;
            andThen.Invoke();
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
