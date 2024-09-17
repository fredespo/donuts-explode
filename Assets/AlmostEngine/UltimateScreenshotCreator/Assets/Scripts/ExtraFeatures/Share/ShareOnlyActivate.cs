using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AlmostEngine.Screenshot
{
	/// Automatically disable the gameobject if the share feature is not available.
    public class ShareOnlyActivate : MonoBehaviour
    {
        void Start()
        {
            if (!ShareUtils.CanShare())
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
