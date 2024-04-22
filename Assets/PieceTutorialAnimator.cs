using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceTutorialAnimator : MonoBehaviour
{
    public GameObject piece;
    public GameObject pieceParent;
    public string pieceSortingLayer;
    private Action onComplete;
    private float[] angles;
    private GameObject spawnedPiece;
    private AudioSource pieceChangeAngleSound;

    void Start()
    {
        this.pieceChangeAngleSound = GetComponent<AudioSource>();
    }

    public void AnimatePieceAndThen(Action callback)
    {
        this.onComplete = callback;
        StartCoroutine(AnimatePieceCoroutine(angles));
    }

    public void SetAngles(float[] angles)
    {
        this.angles = angles;
    }

    private IEnumerator AnimatePieceCoroutine(float[] angles)
    {
        pieceParent.SetActive(true);
        this.spawnedPiece = Instantiate(piece, gameObject.transform.position, gameObject.transform.rotation);
        Animator anim = spawnedPiece.GetComponent<Animator>();
        spawnedPiece.transform.SetParent(pieceParent.transform);
        spawnedPiece.GetComponentInChildren<SpriteRenderer>().sortingLayerName = this.pieceSortingLayer;
        spawnedPiece.transform.position = gameObject.transform.position;
        spawnedPiece.transform.localScale = new Vector3(81, 81, 1);
        spawnedPiece.GetComponent<PolygonCollider2D>().enabled = false;
        spawnedPiece.transform.eulerAngles = new Vector3(spawnedPiece.transform.eulerAngles.x, spawnedPiece.transform.eulerAngles.y, this.angles[0]);
        yield return new WaitForSeconds(0.5f);
        anim.enabled = true;
        anim.SetTrigger("ZoomIn");
        yield return new WaitForSeconds(1);

        for (int i = 1; i < this.angles.Length; ++i)
        {
            Taptic.Light();
            spawnedPiece.transform.eulerAngles = new Vector3(spawnedPiece.transform.eulerAngles.x, spawnedPiece.transform.eulerAngles.y, this.angles[i]);
            pieceChangeAngleSound.Play();
            yield return new WaitForSeconds(0.7f);
        }

        anim.SetTrigger("ZoomOut");
        yield return new WaitForSeconds(1);
        onComplete.Invoke();
    }

    public void DestroySpawnedPiece()
    {
        Destroy(this.spawnedPiece);
        this.spawnedPiece = null;
    }
}
