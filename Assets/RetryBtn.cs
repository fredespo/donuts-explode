using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RetryBtn : MonoBehaviour
{
    [SerializeField] private HUD hud;
    [SerializeField] private GameOverUI ui;

    public void Retry()
    {
        ui.Hide();
        hud.DrinkCoffee();
    }
}
