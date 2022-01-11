using UnityEngine;
using System.Collections;

public class Animation_seq: MonoBehaviour
{
    public float fps = 24.0f;
    public Texture2D[] frames;

    private int frameIndex;
    private MeshRenderer rendererMy;

    void Start()
    {
        rendererMy = GetComponent<MeshRenderer>();
        NextFrame();
        InvokeRepeating("NextFrame", 1 / fps, 1 / fps);
    }

    void NextFrame()
    {
        rendererMy.material.SetTexture("_MainTex", frames[frameIndex]);
        frameIndex = frameIndex + 0001;
        if(frameIndex >= frames.Length)
        {
            Destroy(gameObject);
        }
    }
}