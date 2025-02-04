using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public static int startingLives = 3;
    public DataStorage dataStorage;
    public int livesLeft = 3;
    public Text textNormal;
    public Text textUnlimited;

    // Start is called before the first frame update
    void Start()
    {
        LoadLives();
    }

    void OnEnable()
    {
        LoadLives();
    }

    void LoadLives()
    {
        SetLivesLeft(this.dataStorage.GetLives());
    }

    public void Decrement()
    {
        SetLivesLeft(this.livesLeft - 1);
    }

    public int GetLivesLeft()
    {
        return this.livesLeft;
    }

    private void SetLivesLeft(int value)
    {
        if (this.livesLeft != DataStorage.LIVES_UNLIMITED)
        {
            this.livesLeft = Mathf.Max(0, value);
        }
        RefreshText();
    }

    public void SetLivesToInfinite()
    {
        SetLivesLeft(DataStorage.LIVES_UNLIMITED);
    }

    public void GiveExtraLivesForRewardedAd() {
        const int reward = 3;
        if (this.livesLeft <= 0) {
            SetLivesLeft(reward);
        }
        else {
            SetLivesLeft(this.livesLeft + reward);
        }
    }

    private void RefreshText()
    {
        if (this.livesLeft == DataStorage.LIVES_UNLIMITED)
        {
            this.textNormal.enabled = false;
            this.textUnlimited.enabled = true;
        }
        else
        {
            this.textNormal.enabled = true;
            this.textNormal.text = this.livesLeft.ToString();
            this.textUnlimited.enabled = false;
        }
    }

    public void Reset()
    {
        SetLivesLeft(startingLives);
    }
}
