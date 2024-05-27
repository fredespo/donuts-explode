using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkles : MonoBehaviour
{
    public List<Color> colors;

    // Start is called before the first frame update
    void Start()
    {
        // color all children randomly
        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            sr.color = colors[Random.Range(0, colors.Count)];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
