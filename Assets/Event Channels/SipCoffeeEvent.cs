using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SipCoffeeEvent
{
    private int sipNumber;
    private int totalSips;

    public SipCoffeeEvent(int sipNumber, int totalSips) {
        this.sipNumber = sipNumber;
        this.totalSips = totalSips;
    }

    public int getSipNumber() {
        return this.sipNumber;
    }

    public int getTotalSips() {
        return this.totalSips;
    }
}
