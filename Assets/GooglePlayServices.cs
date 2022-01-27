using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine.Analytics;

public class GooglePlayServices : MonoBehaviour
{
    public UnityEvent afterInitialSignInAttempt;
    private bool signedIn;
    private static string LEADERBOARD_ID = "CgkIx6OD85cGEAIQAQ";
    public bool IsInitialized { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // authenticate user:
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            afterInitialSignInAttempt.Invoke();
            signedIn = result == SignInStatus.Success;
        });
        PlayGamesPlatform.Activate();
        this.IsInitialized = true;
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
            this.SignIn(result => andThen.Invoke());
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
                Social.ReportScore(score, LEADERBOARD_ID, (bool success) => {
                    if (success)
                    {
                        Analytics.CustomEvent("high_score_post_success");
                    }
                    else
                    {
                        Analytics.CustomEvent("high_score_post_fail");
                    }
                    action.Invoke(success);
                });
            }
            else
            {
                action.Invoke(false);
            }
        });
    }

    public void ProcessCurrentHighScore(Action<long> action)
    {
        if(this.signedIn)
        {
            PlayGamesPlatform.Instance.LoadScores
            (
                LEADERBOARD_ID,
                LeaderboardStart.PlayerCentered,
                1,
                LeaderboardCollection.Public,
                LeaderboardTimeSpan.AllTime,
                (LeaderboardScoreData data) =>
                {
                    action.Invoke(data.PlayerScore.value);
                }
            );
        }
    }

    public void SignIn(Action<SignInStatus> andThen)
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => {
            signedIn = result == SignInStatus.Success;
            andThen.Invoke(result);
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
