using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSipSound : MonoBehaviour
{
    public void OnCoffeeSip(SipCoffeeEvent sipCoffeeEvent) {
        if (sipCoffeeEvent.getSipNumber() == sipCoffeeEvent.getTotalSips()) {
            GetComponent<AudioSource>().Play();
        }
    }
}
