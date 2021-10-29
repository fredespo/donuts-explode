using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BombHole : MonoBehaviour
{
    public GameObject filledCollider;
    public float fillDelay = 0.1f;
    private GameObject fillWith;

    void Start()
    {
        GetComponent<BombHolePlacer>().enabled = false;
    }

    public void FillWith(GameObject obj)
    {
        if(this.fillWith != null)
        {
            return;
        }
        this.fillWith = obj;
        StartCoroutine(FillAfterDelay());
    }

    public bool IsFilled()
    {
        return this.fillWith != null;
    }

    private IEnumerator FillAfterDelay()
    {
        yield return new WaitForSeconds(this.fillDelay);
        this.fillWith.GetComponent<BombPiece>().FilledHole();
        Destroy(this.fillWith);
        filledCollider.transform.SetParent(gameObject.transform.parent.transform.parent, true);
        filledCollider.SetActive(true);
        filledCollider.GetComponent<PolygonCollider2D>().enabled = true;
        GameObject.FindWithTag("PieceFitsSoundEffect").GetComponent<AudioSource>().Play(0);
        Destroy(gameObject);
    }
}
