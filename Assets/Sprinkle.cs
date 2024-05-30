using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // set random rotation
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        if (IsOverlappingHole()) {
            Destroy(gameObject);
        }
    }

    bool IsOverlappingHole() {
        Collider2D sprinkle = this.GetComponent<Collider2D>();
        Collider2D[] overlapsWith = Physics2D.OverlapCircleAll(sprinkle.bounds.center, sprinkle.bounds.extents.y);
        foreach(Collider2D other in overlapsWith) {
            if (other.gameObject != gameObject && IsHole(other.gameObject)) {
                return true;
            }
        }
        return false;
    }

    bool IsHole(GameObject obj) {
        return obj.GetComponent<BombHole>() != null;
    }
}
