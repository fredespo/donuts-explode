using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BombHole : MonoBehaviour
{
    public GameObject filledCollider;
    public float fillDelay = 0.1f;
    private GameObject fillWith;
    private GameObject magnet;

    void Start()
    {
        GetComponent<BombHolePlacer>().enabled = false;
        this.magnet = GetChildWithName("Magnet");
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
        Taptic.Vibrate();
        Destroy(gameObject);
    }

    private GameObject GetChildWithName(string name)
    {
        Transform trans = this.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    public GameObject GetMagnet()
    {
        return this.magnet;
    }
}
