using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BombHole : MonoBehaviour
{
    public GameObject filledCollider;

    void Start()
    {
        GetComponent<BombHolePlacer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(gameObject.tag))
        {
            fillWith(col.gameObject);
        }
    }

    public void fillWith(GameObject obj)
    {
        obj.SendMessage("FilledHole");
        Destroy(obj);
        Destroy(gameObject);
        filledCollider.transform.SetParent(gameObject.transform.parent.transform.parent, true);
        filledCollider.SetActive(true);
        filledCollider.GetComponent<PolygonCollider2D>().enabled = true;
        GameObject.FindWithTag("PieceFitsSoundEffect").GetComponent<AudioSource>().Play(0);
    }
}
