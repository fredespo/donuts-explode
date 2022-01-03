using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSelfDestroyer : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestorySelfCoroutine());
    }

    private IEnumerator DestorySelfCoroutine()
    {
        yield return new WaitForSeconds(this.delay);
        Destroy(gameObject);
    }
}
