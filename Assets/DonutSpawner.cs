using BinaryCharm.SemanticColorPalette;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutSpawner : MonoBehaviour
{
    [SerializeField] private GameObject donut;
    [SerializeField] private SCP_PaletteProvider donutPaletteProvider;
    public float minDistX = 300;
    private float prevX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ClearDonuts();
        InvokeRepeating("SpawnDonut", 7, 1);
    }

    void SpawnDonut()
    {
        GameObject spawnedDonut = Instantiate(this.donut, gameObject.transform);
        spawnedDonut.GetComponent<RectTransform>().anchoredPosition = new Vector2(getNextDonutSpawnX(), 300);
        spawnedDonut.GetComponent<Bomb>().SetPalette(UnityEngine.Random.Range(0, this.donutPaletteProvider.GetNumPalettes()));
    }

    private float getNextDonutSpawnX() {
        float xPos = Random.Range(-400, 400);
        if (Mathf.Abs(xPos - this.prevX) < this.minDistX) {
            xPos += (this.prevX < 0 ? this.minDistX : -this.minDistX);
        }
        this.prevX = xPos;
        return xPos;
    }

    void ClearDonuts() {
        foreach (Transform child in this.transform) {
            Destroy(child.gameObject);
        }
    }
}
