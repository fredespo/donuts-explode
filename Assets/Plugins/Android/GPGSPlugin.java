// Place this file in: Assets/Plugins/Android/GPGSPlugin.java
package com.unity3d.player;

import android.app.Activity;
import android.util.Log;
import com.unity3d.player.UnityPlayer;
import com.google.android.gms.games.Games;
import com.google.android.gms.games.GamesSignInClient;
import com.google.android.gms.games.PlayGames;
import com.google.android.gms.games.PlayGamesSdk;
import com.google.android.gms.games.AuthenticationResult;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;

public class GPGSPlugin {
    private static final String CLIENT_OBJECT = "GooglePlayGamesServices";
    private static final String TAG = "GPGSPlugin";
    private static Activity currentActivity;
    private static GamesSignInClient gamesSignInClient;
    private static boolean isSignedIn = false;
    private static boolean isInitialized = false;
    private static boolean isSigningIn = false;

    // Initialize the plugin
    public static void initialize() {
        currentActivity = UnityPlayer.currentActivity;

        // Initialize Play Games SDK
        PlayGamesSdk.initialize(currentActivity);
        gamesSignInClient = PlayGames.getGamesSignInClient(currentActivity);
        isInitialized = true;

        Log.d(TAG, "GPGS Plugin initialized");

        // Check authentication status after a brief delay
        currentActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                checkAuthenticationStatus();
            }
        });
    }

    private static void checkAuthenticationStatus() {
        if (!isInitialized || gamesSignInClient == null) {
            Log.e(TAG, "Plugin not initialized properly");
            return;
        }

        gamesSignInClient.isAuthenticated().addOnCompleteListener(authenticationCompleteListener);
    }

    // Sign in to Google Play Games Services
    public static void signIn() {
        if (currentActivity == null) {
            Log.e(TAG, "Activity is null");
            UnityPlayer.UnitySendMessage(CLIENT_OBJECT, "OnSignInFailed", "Activity is null");
            return;
        }

        if (!isInitialized || gamesSignInClient == null) {
            Log.e(TAG, "Plugin not initialized");
            UnityPlayer.UnitySendMessage(CLIENT_OBJECT, "OnSignInFailed", "Plugin not initialized");
            return;
        }

        if (isSigningIn) {
            Log.d(TAG, "Sign in already in progress");
            return;
        }

        isSigningIn = true;
        Log.d(TAG, "Attempting to sign in...");

        gamesSignInClient.signIn().addOnCompleteListener(authenticationCompleteListener);
    }

    // Single reusable listener for all authentication operations
    private static final OnCompleteListener<AuthenticationResult> authenticationCompleteListener = new OnCompleteListener<AuthenticationResult>() {
        @Override
        public void onComplete(Task<AuthenticationResult> task) {
            isSigningIn = false;
            onSignInComplete(task);
        }
    };

    private static void onSignInComplete(Task<AuthenticationResult> signInTask) {
        if (signInTask.isSuccessful() && signInTask.getResult().isAuthenticated()) {
            isSignedIn = true;
            Log.d(TAG, "Sign in successful");
            UnityPlayer.UnitySendMessage(CLIENT_OBJECT, "OnSignInSuccess", "");
        } else {
            isSignedIn = false;
            String errorMsg = "Unknown error";

            Exception exception = signInTask.getException();
            if (exception != null) {
                errorMsg = exception.getClass().getSimpleName() + ": " + exception.getMessage();
                Log.e(TAG, "Sign in failed: " + errorMsg, exception);
            } else {
                Log.e(TAG, "Sign in failed: Unknown error - task successful: " + signInTask.isSuccessful());
                if (signInTask.getResult() != null) {
                    Log.e(TAG, "Authentication result: " + signInTask.getResult().isAuthenticated());
                }
            }

            UnityPlayer.UnitySendMessage(CLIENT_OBJECT, "OnSignInFailed", errorMsg);
        }
    }

    // Check if signed in
    public static boolean isSignedIn() {
        return isSignedIn;
    }

    // Get more detailed error information
    public static void getLastError() {
        Log.d(TAG, "Initialized: " + isInitialized + ", Signed in: " + isSignedIn);
        if (gamesSignInClient != null) {
            Log.d(TAG, "GamesSignInClient is available");
        } else {
            Log.d(TAG, "GamesSignInClient is null");
        }
    }
}
