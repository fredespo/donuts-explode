using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScoreSavingButton : MonoBehaviour
{
    public InputField nameField;
    public Image buttonImage;
    public Score score;
    public DataStorage dataStorage;
    private EventTrigger eventTrigger;
    private Color buttonColorDisabled;
    private Color buttonColorEnabled;

    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        Color buttonColor = buttonImage.color;
        buttonColorDisabled = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 0.5f);
        buttonColorEnabled = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 1f);
    }

    void Update()
    {
        if(nameField.text.Trim().Length == 0)
        {
            eventTrigger.enabled = false;
            buttonImage.color = buttonColorDisabled;
        }
        else
        {
            eventTrigger.enabled = true;
            buttonImage.color = buttonColorEnabled;
        }
    }

    public void SaveScore()
    {
        dataStorage.SaveHighScore(nameField.text.Trim(), score.GetScore());
    }
}
