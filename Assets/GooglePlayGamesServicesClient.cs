using UnityEngine;
using UnityEngine.UI;

public class GooglePlayGamesServicesClient : MonoBehaviour
{
    // package of the Java plugin class which should be under Assets/Plugins/Android
    private const string PLUGIN_CLASS = "com.unity3d.player.GPGSPlugin";
    public Text errorMsgText;
    public GameObject errorScreen;

    void Start()
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass pluginClass = new AndroidJavaClass(PLUGIN_CLASS);
                pluginClass.CallStatic("initialize");
                ShowErrorMessage("Google Play Games Services initialized.");
            }
        }
        catch (System.Exception e)
        {
            ShowErrorMessage("Failed to initialize Google Play Games Services. Please check your setup: " + e.Message);
        }
    }

    void OnSignInSuccess()
    {
        Debug.Log("Signed in successfully!");
        ShowErrorMessage("Sign in successful!");
    }
    void OnSignInFailed(string errorMessage)
    {
        Debug.Log("Sign in failed!");
        ShowErrorMessage("Sign in failed. Please try again. " + errorMessage);
    }

    private void ShowErrorMessage(string message)
    {
        if (errorScreen != null)
        {
            errorScreen.SetActive(true);
        }

        if (errorMsgText != null)
        {
            errorMsgText.text = message;
        }
    }
}
