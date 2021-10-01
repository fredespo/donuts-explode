using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceTutorialAnimator : MonoBehaviour
{
    public GameObject piece;
    public GameObject pieceParent;
    private Action onComplete;
    private float[] angles;

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
        GameObject spawnedPiece = Instantiate(piece, gameObject.transform.position, gameObject.transform.rotation);
        Animator anim = spawnedPiece.GetComponent<Animator>();
        spawnedPiece.transform.SetParent(pieceParent.transform);
        spawnedPiece.transform.position = gameObject.transform.position;
        spawnedPiece.transform.localScale = new Vector3(81, 81, 1);
        spawnedPiece.GetComponent<PolygonCollider2D>().enabled = false;
        spawnedPiece.transform.eulerAngles = new Vector3(spawnedPiece.transform.eulerAngles.x, spawnedPiece.transform.eulerAngles.y, this.angles[0]);
        yield return new WaitForSeconds(0.8f);
        anim.enabled = true;
        anim.SetTrigger("ZoomIn");
        yield return new WaitForSeconds(2);

        for(int i = 1; i < this.angles.Length; ++i)
        {
            spawnedPiece.transform.eulerAngles = new Vector3(spawnedPiece.transform.eulerAngles.x, spawnedPiece.transform.eulerAngles.y, this.angles[i]);
            yield return new WaitForSeconds(1);
        }

        anim.SetTrigger("ZoomOut");
        yield return new WaitForSeconds(2f);
        Destroy(spawnedPiece);
        onComplete.Invoke();
    }
}
