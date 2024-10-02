using BinaryCharm.SemanticColorPalette;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutSpawner : MonoBehaviour
{
    [SerializeField] private GameObject donut;
    [SerializeField] private SCP_PaletteProvider donutPaletteProvider;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDonut", 1, 1);
    }

    void SpawnDonut()
    {
        GameObject spawnedDonut = Instantiate(this.donut, gameObject.transform);
        spawnedDonut.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-400, 400), 300);
        spawnedDonut.GetComponent<Bomb>().SetPalette(UnityEngine.Random.Range(0, this.donutPaletteProvider.GetNumPalettes()));
    }
}
