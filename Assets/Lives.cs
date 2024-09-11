using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public static int startingLives = 3;
    public DataStorage dataStorage;
    public int livesLeft = 3;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        this.text = GetComponent<Text>();
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
        this.livesLeft = value;
        RefreshText();
    }

    private void RefreshText()
    {
        if (this.text != null)
        {
            this.text.text = this.livesLeft.ToString();
        }
    }

    public void Reset()
    {
        SetLivesLeft(startingLives);
    }
}
