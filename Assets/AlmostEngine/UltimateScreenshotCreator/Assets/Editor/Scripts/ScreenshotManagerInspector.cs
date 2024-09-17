
//#define ULTIMATE_SCREENSHOT_DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

namespace AlmostEngine.Screenshot
{
    [CustomEditor(typeof(ScreenshotManager))]
    public class ScreenshotManagerInspector : Editor
    {
        protected ScreenshotManager m_ScreenshotManager;



        ScreenshotConfigDrawer m_ConfigDrawer;

        public virtual void OnEnable()
        {
            m_ScreenshotManager = (ScreenshotManager)target;

            m_ConfigDrawer = new ScreenshotConfigDrawer();
            m_ConfigDrawer.Init(serializedObject, m_ScreenshotManager, m_ScreenshotManager.m_Config, serializedObject.FindProperty("m_Config"));


        }



        public override void OnInspectorGUI()
        {
            // catch events
            m_ScreenshotManager.HandleEditorHotkeys();


            serializedObject.Update();


            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawCaptureModeGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawFolderGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawNameGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            if (m_ScreenshotManager.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW && m_ScreenshotManager.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL && m_ScreenshotManager.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                m_ConfigDrawer.DrawResolutionGUI();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
            }

            if (m_ScreenshotManager.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL && m_ScreenshotManager.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                m_ConfigDrawer.DrawCamerasGUI();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
            }

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawOverlaysGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();


            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawCompositionGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();


            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawBatchesGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawPreviewGUI();
            if (GUILayout.Button("Update"))
            {
                m_ScreenshotManager.UpdatePreview();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();


            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawCaptureGUI();
            DrawCaptureButtonsGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawDelay();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawHotkeysGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawShareGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawUtilsGUI();
            EditorGUILayout.Separator();
            if (GUILayout.Button("Reset state"))
            {
                m_ScreenshotManager.Reset();
            }
            if (GUILayout.Button("Clear cache"))
            {
                m_ScreenshotManager.ClearCache();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawUsage();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawFeatureExclude();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();



#if ULTIMATE_SCREENSHOT_DEBUG
						EditorGUILayout.BeginVertical (GUI.skin.box);
						DrawDebugGUI ();
						EditorGUILayout.EndVertical ();
						EditorGUILayout.Separator ();
#endif

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            ScreenshotWindow.DrawSupportGUI();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            ScreenshotWindow.DrawContactGUI();

            serializedObject.ApplyModifiedProperties();


            //			DrawDefaultInspector ();
        }


        protected void DrawCaptureButtonsGUI()
        {

            EditorGUILayout.BeginHorizontal();

            Color c = GUI.color;
            GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
            if (m_ScreenshotManager.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST && !Application.isPlaying)
            {
                GUI.enabled = false;
            }
            if (GUILayout.Button(GetCaptureButtonText(), GUILayout.Height(50)))
            {
                if (m_ScreenshotManager.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST && m_ScreenshotManager.m_IsBurstActive == true)
                {
                    m_ScreenshotManager.StopBurst();
                }
                else
                {
                    m_ScreenshotManager.Capture();
                }
            }
            GUI.enabled = true;
            GUI.color = c;


            if (GUILayout.Button("Show", GUILayout.MaxWidth(70), GUILayout.Height(50)))
            {
                EditorUtility.RevealInFinder(m_ScreenshotManager.m_Config.GetPath());
            }
            EditorGUILayout.EndHorizontal();

        }

        string GetCaptureButtonText()
        {
            if (m_ScreenshotManager.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST && m_ScreenshotManager.m_IsBurstActive == true)
            {
                return "Stop";
            }
            else if (m_ScreenshotManager.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST)
            {
                return "Start Burst";
            }
            else
            {
                return "Capture";
            }
        }




        #region DEBUG

        void DrawDebugGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_IsBurstActive"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_IsCapturing"));

            EditorGUILayout.LabelField("Overlays");
            foreach (ScreenshotOverlay k in m_ScreenshotManager.m_Config.m_Overlays)
            {
                EditorGUILayout.LabelField("[" + k.m_Canvas.ToString() + "] " + k.m_SettingStack.Count);
            }

            EditorGUILayout.LabelField("Cameras");
            foreach (ScreenshotCamera k in m_ScreenshotManager.m_Config.m_Cameras)
            {
                EditorGUILayout.LabelField("[" + k.m_Camera.ToString() + "] " + k.m_SettingStack.Count);
            }

            EditorGUILayout.Separator();


        }



        #endregion


    }
}
