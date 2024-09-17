using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
#endif

namespace AlmostEngine
{
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
	[ExecuteInEditMode]
#endif
    public class AutoSwitchToNewInputSystem : MonoBehaviour
    {
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
        void Start()
        {
            if (GetComponent<StandaloneInputModule>() != null) {
                GameObject.DestroyImmediate(GetComponent<StandaloneInputModule>());
                this.gameObject.AddComponent<InputSystemUIInputModule>();
                GameObject.DestroyImmediate(this);
            }
        }
#endif
    }
}