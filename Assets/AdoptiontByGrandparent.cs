using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdoptiontByGrandparent : MonoBehaviour
{
    public int generationsToGoBackFromGrandparent;
    public float delay;

    void Start()
    {
        StartCoroutine(GetAdoptedByGrandparentsCoroutine(this.delay, this.generationsToGoBackFromGrandparent));      
    }

    private IEnumerator GetAdoptedByGrandparentsCoroutine(float delay, int generationsToGoBackFromGrandparent)
    {
        yield return new WaitForSeconds(delay);
        GetAdoptedByGrandparents(generationsToGoBackFromGrandparent);
    }

    private void GetAdoptedByGrandparents(int generationsToGoBackFromGrandparent)
    {
        Transform newParent = this.transform.parent.parent.transform;
        for(int i = 0; i < generationsToGoBackFromGrandparent; ++i)
        {
            newParent = newParent.transform.parent.transform;
        }
        this.transform.parent = newParent;
    }
}
