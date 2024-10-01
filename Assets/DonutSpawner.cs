using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutSpawner : MonoBehaviour
{
    [SerializeField] private GameObject donut;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDonut", 1, 1);
    }

    void SpawnDonut()
    {
        GameObject spawnedDonut = Instantiate(this.donut, gameObject.transform);
        RectTransform donutRectTransform = spawnedDonut.GetComponent<RectTransform>();
        donutRectTransform.anchoredPosition = new Vector2(Random.Range(-400, 400), 300);
    }

}
