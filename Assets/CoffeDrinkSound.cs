using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeDrinkSound : MonoBehaviour
{
    public void OnCoffeeSip(SipCoffeeEvent sipCoffeeEvent) {
        if (sipCoffeeEvent.getSipNumber() > 0 && sipCoffeeEvent.getSipNumber() < sipCoffeeEvent.getTotalSips()) {
            GetComponent<AudioSource>().Play();
        }
    }
}
