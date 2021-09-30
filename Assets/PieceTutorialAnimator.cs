using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceTutorialAnimator : MonoBehaviour
{
    public GameObject piece;
    public GameObject pieceParent;
    private Action onComplete;

    public void AnimatePieceAndThen(Action callback)
    {
        this.onComplete = callback;
        StartCoroutine(AnimatePieceCoroutine());
    }

    private IEnumerator AnimatePieceCoroutine()
    {
        GameObject spawnedPiece = Instantiate(piece, gameObject.transform.position, gameObject.transform.rotation);
        Animator anim = spawnedPiece.GetComponent<Animator>();
        spawnedPiece.transform.SetParent(pieceParent.transform);
        spawnedPiece.transform.position = gameObject.transform.position;
        spawnedPiece.transform.localScale = new Vector3(81, 81, 1);
        spawnedPiece.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.8f);
        anim.enabled = true;
        anim.SetTrigger("ZoomIn");
        yield return new WaitForSeconds(2);
        anim.SetTrigger("ZoomOut");
        yield return new WaitForSeconds(2f);
        Destroy(spawnedPiece);
        onComplete.Invoke();
    }
}
