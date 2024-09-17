#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;


namespace AlmostEngine.Screenshot
{
    /// <summary>
    /// Game View Utils is used to control the game view sizes.
    /// Part of this code was inspired and adapted from http://answers.unity3d.com/questions/956123/add-and-select-game-view-resolution.html
    /// </summary>
    public static class GameViewUtils
    {

        public enum SizeType
        {
            RATIO,
            FIXED_RESOLUTION
        }
        ;

        #region SizeGroups Management


        public static object GetCurrentGameViewSizeGroup()
        {
            // Get the instance of the asset of type GameViewSizes containing all gameview size data						 
            var sizesType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizes");
            var instanceProp = sizesType.GetProperty("instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.FlattenHierarchy);
            object gameViewSizesInstance = instanceProp.GetValue(null, null);

            // Get the current group GameViewSizeGroup
            // containing resolutions data for Standalone, or iOS, or Android ...
            var currentGroupProp = sizesType.GetProperty("currentGroup", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            return currentGroupProp.GetValue(gameViewSizesInstance, null);
        }

        public static int GetCurrentSizeIndex()
        {
            var gameviewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            var selectedSizeIndex = gameviewType.GetProperty("selectedSizeIndex", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            var gameView = GetGameView();
            return (int)selectedSizeIndex.GetValue(gameView, null);
        }

        public static void SetCurrentSizeIndex(int index)
        {
            var gameviewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            var gameView = GetGameView();
            //		#if (UNITY_5_4_OR_NEWER && UNITY_STANDALONE)
            //		var SizeSelectionCallback = gameviewType.GetMethod ("SizeSelectionCallback", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            //		SizeSelectionCallback.Invoke (gameView, new object[] { index, null });
            //		#else
            var selectedSizeIndex = gameviewType.GetProperty("selectedSizeIndex", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            selectedSizeIndex.SetValue(gameView, index, null);
            //		#endif
        }

        public static List<string>
        GetAllSizeNames()
        {
            List<string> names = new List<string>();
            object group = GetCurrentGameViewSizeGroup();
            System.Reflection.MethodInfo getTotalCount = group.GetType().GetMethod("GetTotalCount", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            System.Reflection.MethodInfo getGameViewSize = group.GetType().GetMethod("GetGameViewSize", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            int size = (int)getTotalCount.Invoke(group, null);
            for (int i = 0; i < size; i++)
            {
                object gameViewSize = getGameViewSize.Invoke(group, new object[] { i });
                string name = (string)gameViewSize.GetType().GetProperty("baseText", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(gameViewSize, null);
                names.Add(name);
            }
            return names;
        }

        public static int FindSize(string baseText)
        {
            List<string> names = GetAllSizeNames();

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i] == baseText)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void AddCustomSize(SizeType sizeType, int width, int height, string baseText)
        {
            // Create a new game view size
            var sizesType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSize");
            var gameviewsizetypeType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizeType");
            System.Reflection.ConstructorInfo constructor = sizesType.GetConstructor(new System.Type[] {
                                gameviewsizetypeType,
                                typeof(int),
                                typeof(int),
                                typeof(string)
                        });
            object customGameViewSize = constructor.Invoke(new object[] {
                                (int)sizeType,
                                width,
                                height,
                                baseText
                        });

            // Add it to the view size group
            object group = GetCurrentGameViewSizeGroup();
            System.Reflection.MethodInfo addCustomSize = group.GetType().GetMethod("AddCustomSize", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            addCustomSize.Invoke(group, new object[] { customGameViewSize });
        }

        public static void RemoveCustomSize(int id)
        {
            object group = GetCurrentGameViewSizeGroup();
            System.Reflection.MethodInfo removeCustomSize = group.GetType().GetMethod("RemoveCustomSize", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            removeCustomSize.Invoke(group, new object[] { id });
        }

        public static bool SizeExists(string name)
        {
            return FindSize(name) != -1;
        }

        public static SizeType GetCurrentSizeType()
        {
            object group = GetCurrentGameViewSizeGroup();
            System.Reflection.MethodInfo getGameViewSize = group.GetType().GetMethod("GetGameViewSize", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            int i = GetCurrentSizeIndex();
            object gameViewSize = getGameViewSize.Invoke(group, new object[] { i });

            return (SizeType)gameViewSize.GetType().GetProperty("sizeType", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(gameViewSize, null);
        }

        #endregion

        #region Size Management

        public static void SetGameViewSize(int width, int height)
        {
            var gameviewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            var gameView = GetGameView();




            var currentGameViewSizeProp = gameviewType.GetProperty("currentGameViewSize", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            if (currentGameViewSizeProp == null)
            {
                Debug.LogError("Can not get currentGameViewSize prop");
                return;
            }

            var currentGameViewSize = currentGameViewSizeProp.GetValue(gameView, null);
            var gameViewSizeType = currentGameViewSize.GetType();

            var w = gameViewSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var h = gameViewSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            w.SetValue(currentGameViewSize, width, null);
            h.SetValue(currentGameViewSize, height, null);

            // Force repaint
            gameView.Repaint();
        }

        public static Vector2 GetCurrentGameViewSize()
        {
            var gameView = GetGameView();

            var currentGameViewSizeProp = gameView.GetType().GetProperty("currentGameViewSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var currentGameViewSize = currentGameViewSizeProp.GetValue(gameView, null);
            var gameViewSizeType = currentGameViewSize.GetType();

            var w = gameViewSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var h = gameViewSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            Vector2 size = new Vector2();
            size.x = (int)w.GetValue(currentGameViewSize, new object[0] { });
            size.y = (int)h.GetValue(currentGameViewSize, new object[0] { });
            return size;

        }

        public static Vector2 GetCurrentGameViewRectSize()
        {
            var gameviewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            var gameView = GetGameView();

#if (UNITY_5_4_OR_NEWER)
            var targetSize = gameviewType.GetProperty("targetSize", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            return (Vector2)targetSize.GetValue(gameView, null);
#else
						var method = gameviewType.GetMethod ("GetConstrainedGameViewRenderRect", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
						Rect size = (Rect)method.Invoke (gameView, null);
						return size.size;
#endif
        }

        public static EditorWindow GetGameView()
        {
            var gameviewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            return EditorWindow.GetWindow(gameviewType);
        }

        #endregion

        #region Rect Management

        public static Rect GetCurrentGameViewRect()
        {
            return GetGameView().position;
        }

        public static void SetGameViewRect(Rect pos)
        {
            GetGameView().position = pos;
        }


        public static SceneView GetSceneView()
        {
            return SceneView.lastActiveSceneView;
        }

        public static Vector2 GetSceneViewSize()
        {
            var rect = SceneView.lastActiveSceneView.camera.pixelRect;
            return new Vector2(rect.width, rect.height);
        }


        // public static Color[] ReadScreenPixel(int x0, int y0, int widht, int height)
        // {
            
        //     var assembly = typeof(EditorWindow).Assembly;
        //     var T = assembly.GetType("InternalEditorUtility");
        //     var method = T.GetMethod("ReadScreenPixel", BindingFlags.Static);
        //     return method.Invoke(null, new System.Object[] { new Vector2(x0, y0), widht, height });
        // }


        #endregion

    }
}

#endif