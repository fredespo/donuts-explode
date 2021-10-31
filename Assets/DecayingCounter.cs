using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DecayingCounter : MonoBehaviour
{
    public int count = 0;
    public int targetCount = 5;
    public float resetAfterSeconds = 1f;
    public UnityEvent onTargetReached;

    void Start()
    {
        
    }

    public void IncrementCount()
    {
        if(this.count == 0)
        {
            StartCoroutine(ResetCoroutine());
        }
        this.count++;
        if(this.count == this.targetCount)
        {
            this.onTargetReached.Invoke();
            ResetCount();
        }
    }

    private void ResetCount()
    {
        this.count = 0;
    }

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(this.resetAfterSeconds);
        ResetCount();
    }
}
