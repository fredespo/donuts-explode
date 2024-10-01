using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public float speed;
    public GameObject[] chunks;

    public float[] angles;

    public void SpawnChunks()
    {
        for (int i = 0; i < this.chunks.Length; ++i)
        {
            GameObject spawnedChunk = Instantiate(this.chunks[i], gameObject.transform.parent.parent);
            RectTransform chunkRectTransform = spawnedChunk.GetComponent<RectTransform>();
            chunkRectTransform.anchorMin = new Vector2(0.5f, 1f);
            chunkRectTransform.anchorMax = new Vector2(0.5f, 1f);
            chunkRectTransform.anchoredPosition = new Vector2(0, -400);
            chunkRectTransform.localScale = new Vector2(81, 81);
            float directionAngle = this.angles[i];
            float rotationAngle = directionAngle + 90f;
            chunkRectTransform.rotation = Quaternion.Euler(0, 0, rotationAngle);
            float directionAngleRadians = directionAngle * Mathf.Deg2Rad;
            Vector2 force = new Vector2(Mathf.Cos(directionAngleRadians), Mathf.Sin(directionAngleRadians)) * this.speed;
            spawnedChunk.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }
}

