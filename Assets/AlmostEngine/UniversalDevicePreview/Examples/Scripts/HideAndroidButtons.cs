using UnityEngine;
using System.Collections;

namespace AlmostEngine.Examples
{
	/// <summary>
	/// Hide android buttons.
	/// https://forum.unity3d.com/threads/released-immersive-mode-for-android.244659/
	/// </summary>
	public class HideAndroidButtons : MonoBehaviour
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		const int SYSTEM_UI_FLAG_IMMERSIVE_STICKY = 4096;
		const int SYSTEM_UI_FLAG_HIDE_NAVIGATION = 2;
		const int SYSTEM_UI_FLAG_FULLSCREEN = 4;
		
		AndroidJavaObject decorView;
		
		void Awake()
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject window = activity.Call<AndroidJavaObject>("getWindow");
			decorView = window.Call<AndroidJavaObject>("getDecorView");
			
			TurnImmersiveModeOn();
		}
		void OnApplicationFocus(bool focusStatus) {
			if(focusStatus){
				TurnImmersiveModeOn();
			}
		}
		void TurnImmersiveModeOn()
		{
			decorView.Call("setSystemUiVisibility", SYSTEM_UI_FLAG_FULLSCREEN | SYSTEM_UI_FLAG_HIDE_NAVIGATION | SYSTEM_UI_FLAG_IMMERSIVE_STICKY);
		}
		void OnDestroy()
		{
			decorView.Dispose();
		}
		#endif
	}
}