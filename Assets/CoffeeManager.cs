using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeManager : MonoBehaviour
{
    private static int UNLIMITED = 9999;
    public DataStorage dataStorage;

    public void GrantUnlimitedCoffee()
    {
        dataStorage.SetLives(UNLIMITED);
        dataStorage.Save();
        Debug.Log("Unlimited coffee granted");
    }
}
