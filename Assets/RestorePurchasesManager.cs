using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestorePurchasesManager : MonoBehaviour
{
    [SerializeField] private IAP iap;
    [SerializeField] private GameObject restorePurchasesButton;
    [SerializeField] private GameObject restorePurchasesButtonInProgress;
    [SerializeField] private VoidEventChannel restorePurchasesDoneChannel;

    void OnEnable()
    {
        this.restorePurchasesButtonInProgress.SetActive(iap.IsPurchaseRestoreInProgress());
        this.restorePurchasesButton.SetActive(ShouldShowRestorePurchasesButton());
        this.restorePurchasesDoneChannel.OnEventRaised += RestorePurchasesDone;
    }

    void OnDisable()
    {
        this.restorePurchasesDoneChannel.OnEventRaised -= RestorePurchasesDone;
    }

    private void RestorePurchasesDone()
    {
        this.restorePurchasesButtonInProgress.SetActive(false);
        this.restorePurchasesButton.SetActive(ShouldShowRestorePurchasesButton());
    }

    private bool ShouldShowRestorePurchasesButton()
    {
        bool isApplePlatform = Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer;
        return isApplePlatform && !iap.IsPurchaseRestoreInProgress();
    }
}
