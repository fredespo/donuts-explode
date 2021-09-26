using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GooglePlayGames.BasicApi;

public class GooglePlayServicesSignInOut : MonoBehaviour
{
    public UnityEvent onNetworkError;
    public GooglePlayServices googlePlayServices;
    public Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (googlePlayServices.IsSignedIn())
        {
            googlePlayServices.SignOut();
        }
        else
        {
            googlePlayServices.SignIn(result => {
                if(result == SignInStatus.NetworkError)
                {
                    this.onNetworkError.Invoke();
                }
            });
        }
    }

    void Update()
    {
        if (googlePlayServices.IsSignedIn())
        {
            buttonText.text = "Sign out";
        }
        else
        {
            buttonText.text = "Sign in";
        }
    }
}
