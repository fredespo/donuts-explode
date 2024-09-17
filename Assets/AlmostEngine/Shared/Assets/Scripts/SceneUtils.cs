using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AlmostEngine
{
    public class SceneUtils
    {
        public static string GetSceneName(int id)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(id);
            string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
            return sceneName;
        }

        public static List<T> FindAllSceneObjects<T>(bool getInactive = true)
            where T : Component
        {
            List<T> objectsInScene = new List<T>();
            Object[] objs = Resources.FindObjectsOfTypeAll(typeof(T));

            foreach (Object go in objs)
            {
                if (IsPrefab(((Component)go).gameObject))
                {
                    // Debug.Log(go.name + " IS PREFAB");
                    continue;
                }

                // if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                // 	continue;

                // #if UNITY_EDITOR
                // // Ignore prefabs
                // if (EditorUtility.IsPersistent(((Component)go).transform.root.gameObject))
                // 	continue;
                // #endif

                if (!getInactive && !((Component)go).gameObject.activeInHierarchy)
                // if (!getInactive && !((Component)go).gameObject.activeSelf)
                {
                    continue;
                }

                objectsInScene.Add((T)go);
            }

            return objectsInScene;
        }


        public static bool IsPrefab(GameObject obj)
        {
            return (obj.scene.name == null);
        }

    }
}
