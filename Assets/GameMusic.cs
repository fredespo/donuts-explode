using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public float minMusicPitch = 1.0f;
    public float maxMusicPitch = 2.0f;
    [SerializeField] public AnimationCurve windDownCurve;
    public float timeToWindDown = 1.0f;
    private bool windingDown;
    private float windingDownTimer;
    private float windingDownStartPitch;
    public float curveVal;
    private AudioSource music;

    public void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if(windingDown)
        {
            windingDownTimer += Time.deltaTime;
            curveVal = windDownCurve.Evaluate(windingDownTimer / timeToWindDown);
            music.pitch = minMusicPitch + ((windingDownStartPitch - minMusicPitch) * curveVal);
        }
        else
        {
            curveVal = 0;
        }
    }

    public void WindDown()
    {
        if (!windingDown)
        {
            windingDownTimer = 0f;
            windingDown = true;
            windingDownStartPitch = music.pitch;
        }
    }

    public void Reset()
    {
        windingDown = false;
        music.pitch = minMusicPitch;
    }
}
