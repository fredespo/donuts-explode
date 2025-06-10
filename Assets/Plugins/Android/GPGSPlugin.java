// Place this file in: Assets/Plugins/Android/GPGSPlugin.java
package com.unity3d.player;

import android.app.Activity;
import android.util.Log;
import com.google.android.gms.games.Games;
import com.google.android.gms.games.GamesSignInClient;
import com.google.android.gms.games.PlayGames;
import com.google.android.gms.games.PlayGamesSdk;
import com.google.android.gms.games.AuthenticationResult;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;

public class GPGSPlugin {
    private static final String TAG = "GPGSPlugin";
    private static Activity currentActivity;
    private static GamesSignInClient gamesSignInClient;
    private static boolean isSignedIn = false;

    // Initialize the plugin
    public static void initialize() {
        currentActivity = UnityPlayer.currentActivity;

        // Initialize Play Games SDK
        PlayGamesSdk.initialize(currentActivity);
        gamesSignInClient = PlayGames.getGamesSignInClient(currentActivity);

        Log.d(TAG, "GPGS Plugin initialized");
    }

    // Sign in to Google Play Games Services
    public static void signIn() {
        if (currentActivity == null) {
            Log.e(TAG, "Activity is null");
            return;
        }

        gamesSignInClient.isAuthenticated().addOnCompleteListener(new OnCompleteListener<AuthenticationResult>() {
            @Override
            public void onComplete(Task<AuthenticationResult> task) {
                if (task.isSuccessful() && task.getResult().isAuthenticated()) {
                    // Already signed in
                    isSignedIn = true;
                    Log.d(TAG, "Already signed in to GPGS");
                    UnityPlayer.UnitySendMessage("GPGSManager", "OnSignInSuccess", "");
                } else {
                    // Need to sign in
                    gamesSignInClient.signIn().addOnCompleteListener(new OnCompleteListener<AuthenticationResult>() {
                        @Override
                        public void onComplete(Task<AuthenticationResult> signInTask) {
                            if (signInTask.isSuccessful() && signInTask.getResult().isAuthenticated()) {
                                isSignedIn = true;
                                Log.d(TAG, "Sign in successful");
                                UnityPlayer.UnitySendMessage("GPGSManager", "OnSignInSuccess", "");
                            } else {
                                isSignedIn = false;
                                Log.e(TAG, "Sign in failed: " + signInTask.getException());
                                UnityPlayer.UnitySendMessage("GPGSManager", "OnSignInFailed", "");
                            }
                        }
                    });
                }
            }
        });
    }

    // Check if signed in
    public static boolean isSignedIn() {
        return isSignedIn;
    }
}
