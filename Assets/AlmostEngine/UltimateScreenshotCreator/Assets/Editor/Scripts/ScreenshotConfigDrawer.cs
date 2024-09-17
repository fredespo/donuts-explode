using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

using AlmostEngine.ExcludeFromBuildTool;

namespace AlmostEngine.Screenshot
{
    public class ScreenshotConfigDrawer
    {
        public ReorderableList m_ResolutionReorderableList;
        public ReorderableList m_OverlayReorderableList;
        public ReorderableList m_CameraReorderableList;

        SerializedProperty m_ShareSubject;
        SerializedProperty m_ShareText;
        SerializedProperty m_DestinationFolder;
        SerializedProperty m_NumberLeftPadding;
        SerializedProperty m_ExportToPhoneGallery;
        SerializedProperty m_ExportAsync;
        SerializedProperty m_FileFormat;
        SerializedProperty m_ColorFormat;
        SerializedProperty m_RecomputeAlphaLayer;
        SerializedProperty m_DisableHDRPFX;
        SerializedProperty m_JPGQuality;
        SerializedProperty m_CaptureMode;
        SerializedProperty m_AntiAliasing;
        SerializedProperty m_Cameras;
        SerializedProperty m_CameraMode;
        SerializedProperty m_ExportToDifferentLayers;
        SerializedProperty m_Resolutions;
        SerializedProperty m_ResolutionCaptureMode;
        SerializedProperty m_Overlays;
        SerializedProperty m_CaptureActiveUICanvas;
        SerializedProperty m_ForceUICullingLayer;
        SerializedProperty m_PreviewInGameViewWhilePlaying;
        SerializedProperty m_ShowGuidesInPreview;
        SerializedProperty m_GuideCanvas;
        SerializedProperty m_GuidesColor;
        SerializedProperty m_ShowPreview;
        SerializedProperty m_PreviewSize;
        SerializedProperty m_ShotMode;
        SerializedProperty m_MaxBurstShotsNumber;
        SerializedProperty m_MaxShotPerSeconds;
        SerializedProperty m_FixedFrameRate;
        SerializedProperty m_PlaySoundOnCapture;
        SerializedProperty m_DontDestroyOnLoad;
        SerializedProperty m_ShotSound;
        SerializedProperty m_StopTimeOnCapture;
        SerializedProperty m_OverwriteFiles;

        ScreenshotConfig m_Config;
        SerializedObject serializedObject;

        Object m_Obj;

        public bool m_ShowDetailedDevice = false;
        public bool m_Expanded = false;

        public DeviceSelector m_Selector;


        public void Init(SerializedObject s, Object obj, ScreenshotConfig config, SerializedProperty configProperty)
        {
            serializedObject = s;
            m_Obj = obj;
            m_Config = config;

            m_ShareText = configProperty.FindPropertyRelative("m_ShareText");
            m_ShareSubject = configProperty.FindPropertyRelative("m_ShareSubject");
            m_DestinationFolder = configProperty.FindPropertyRelative("m_DestinationFolder");
            m_NumberLeftPadding = configProperty.FindPropertyRelative("m_NumberLeftPadding");
            m_ExportToPhoneGallery = configProperty.FindPropertyRelative("m_ExportToPhoneGallery");
            m_ExportAsync = configProperty.FindPropertyRelative("m_ExportAsync");
            m_FileFormat = configProperty.FindPropertyRelative("m_FileFormat");
            m_ColorFormat = configProperty.FindPropertyRelative("m_ColorFormat");
            m_RecomputeAlphaLayer = configProperty.FindPropertyRelative("m_RecomputeAlphaLayer");
            m_DisableHDRPFX = configProperty.FindPropertyRelative("m_DisableHDRPFX");
            m_JPGQuality = configProperty.FindPropertyRelative("m_JPGQuality");
            m_CaptureMode = configProperty.FindPropertyRelative("m_CaptureMode");
            m_AntiAliasing = configProperty.FindPropertyRelative("m_MultisamplingAntiAliasing");
            m_Cameras = configProperty.FindPropertyRelative("m_Cameras");
            m_CameraMode = configProperty.FindPropertyRelative("m_CameraMode");
            m_ExportToDifferentLayers = configProperty.FindPropertyRelative("m_ExportToDifferentLayers");
            m_Resolutions = configProperty.FindPropertyRelative("m_Resolutions");
            m_ResolutionCaptureMode = configProperty.FindPropertyRelative("m_ResolutionCaptureMode");
            m_Overlays = configProperty.FindPropertyRelative("m_Overlays");
            m_CaptureActiveUICanvas = configProperty.FindPropertyRelative("m_CaptureActiveUICanvas");
            m_ForceUICullingLayer = configProperty.FindPropertyRelative("m_ForceUICullingLayer");
            m_PreviewInGameViewWhilePlaying = configProperty.FindPropertyRelative("m_PreviewInGameViewWhilePlaying");
            m_ShowGuidesInPreview = configProperty.FindPropertyRelative("m_ShowGuidesInPreview");
            m_GuideCanvas = configProperty.FindPropertyRelative("m_GuideCanvas");
            m_GuidesColor = configProperty.FindPropertyRelative("m_GuidesColor");
            m_ShowPreview = configProperty.FindPropertyRelative("m_ShowPreview");
            m_PreviewSize = configProperty.FindPropertyRelative("m_PreviewSize");
            m_ShotMode = configProperty.FindPropertyRelative("m_ShotMode");
            m_MaxBurstShotsNumber = configProperty.FindPropertyRelative("m_MaxBurstShotsNumber");
            m_MaxShotPerSeconds = configProperty.FindPropertyRelative("m_MaxShotPerSeconds");
            m_FixedFrameRate = configProperty.FindPropertyRelative("m_FixedFrameRate");
            m_PlaySoundOnCapture = configProperty.FindPropertyRelative("m_PlaySoundOnCapture");
            m_DontDestroyOnLoad = configProperty.FindPropertyRelative("m_DontDestroyOnLoad");

            m_ShotSound = configProperty.FindPropertyRelative("m_ShotSound");
            m_StopTimeOnCapture = configProperty.FindPropertyRelative("m_StopTimeOnCapture");
            m_OverwriteFiles = configProperty.FindPropertyRelative("m_OverwriteFiles");



            CreateResolutionReorderableList();
            CreateOverlayList();
            CreateCameraReorderableList();


            m_Selector = new DeviceSelector();
            m_Selector.Init(m_Config);

        }


        #region FOLDERS

        string newPath = "";

        public void DrawFolderGUI()
        {
            // Title
            var title = IconsUtils.TryGetIcon("FolderEmpty Icon");
            title.text = " Export Folder".ToUpper();
            m_Config.m_ShowDestination = EditorGUILayout.Foldout(m_Config.m_ShowDestination, title);
            // m_Config.m_ShowDestination = EditorGUILayout.Foldout(m_Config.m_ShowDestination, "EXPORT FOLDER".ToUpper());
            if (m_Config.m_ShowDestination == false)
                return;
            EditorGUILayout.Separator();

            // Select destination type
            EditorGUILayout.PropertyField(m_ExportToPhoneGallery);
            EditorGUILayout.PropertyField(m_ExportAsync);
            bool neediOSGalleryPermission = !RemovePermissionNeeds.IsSymbolDefined(BuildTargetGroup.iOS, "USC_EXCLUDE_IOS_GALLERY");
            if (m_Config.m_ExportToPhoneGallery && usage != null && !neediOSGalleryPermission)
            {
                EditorGUILayout.HelpBox("You can not add the screenshot to the gallery when gallery permission is disabled. Update the permission settings below.", MessageType.Warning);
            }
            EditorGUILayout.PropertyField(m_DestinationFolder);


            if (m_Config.m_DestinationFolder == ScreenshotNameParser.DestinationFolder.PICTURES_FOLDER && !UnityEditor.PlayerSettings.Android.forceSDCardPermission)
            {
                EditorGUILayout.HelpBox("Android storage permission is required to export to the pictures folder. Update the permission settings below.", MessageType.Warning);
            }



            // Path
            if (m_Config.m_DestinationFolder == ScreenshotNameParser.DestinationFolder.CUSTOM_FOLDER)
            {

                EditorGUILayout.BeginHorizontal();

                // Path
                newPath = EditorGUILayout.TextField(m_Config.m_RootedPath);
                if (newPath != m_Config.m_RootedPath)
                {
                    m_Config.m_RootedPath = newPath;
                    EditorUtility.SetDirty(m_Obj);
                }

                // Browse button
                if (GUILayout.Button("Browse", GUILayout.MaxWidth(70)))
                {
                    newPath = EditorUtility.OpenFolderPanel("Select destination folder", m_Config.m_RootedPath, m_Config.m_RootedPath);
                    if (newPath != m_Config.m_RootedPath)
                    {
                        m_Config.m_RootedPath = newPath;
                        EditorUtility.SetDirty(m_Obj);

                        // Dirty hack
                        // The TextField is conflicting with the browse field:
                        // if the textfield is selected then it will not be updated after the folder selection.
                        GUI.FocusControl("");
                    }
                }
                EditorGUILayout.EndHorizontal();

            }
            else
            {
                EditorGUILayout.BeginHorizontal();

                // Path
                newPath = EditorGUILayout.TextField(m_Config.m_RelativePath);
                if (newPath != m_Config.m_RelativePath)
                {
                    m_Config.m_RelativePath = newPath;
                    EditorUtility.SetDirty(m_Obj);
                }

                EditorGUILayout.EndHorizontal();
            }


            if (GUILayout.Button("Open Folder"))
            {
                EditorUtility.RevealInFinder(m_Config.GetPath());
            }

            // Warning message
            if (!PathUtils.IsValidPath(m_Config.GetPath()))
            {
                EditorGUILayout.HelpBox("Path \"" + m_Config.GetPath() + "\" is invalid.", MessageType.Warning);
            }

        }

        #endregion



        #region NAMES

        void OnNameSelectCallback(object target)
        {
            m_Config.m_FileName += (string)target;

            // Dirty hack
            // The TextField is conflicting with the browse field:
            // if the textfield is selected then it will not be updated after the folder selection.
            GUI.FocusControl("");
        }

        string fullName = "";

        public void DrawNameGUI()
        {
            //Title
            // m_Config.m_ShowName = EditorGUILayout.Foldout(m_Config.m_ShowName, "File Name".ToUpper());
            var title = IconsUtils.TryGetIcon("Font Icon");
            title.text = " File Name".ToUpper();
            m_Config.m_ShowName = EditorGUILayout.Foldout(m_Config.m_ShowName, title);
            if (m_Config.m_ShowName == false)
                return;
            EditorGUILayout.Separator();


            // Name
            EditorGUILayout.BeginHorizontal();

            newPath = EditorGUILayout.TextField(m_Config.m_FileName);
            if (newPath != m_Config.m_FileName)
            {
                m_Config.m_FileName = newPath;
                EditorUtility.SetDirty(m_Obj);
            }

            // Create Name Examples Menu
            if (GUILayout.Button("Symbols", GUILayout.MaxWidth(70)))
            {
                var menu = new GenericMenu();
                foreach (ScreenshotNamePresets.NamePreset path in ScreenshotNamePresets.m_NamePresets)
                {

                    menu.AddItem(new GUIContent(path.m_Description), false, OnNameSelectCallback, path.m_Path);
                }
                menu.ShowAsContext();
            }

            EditorGUILayout.EndHorizontal();

            // PADDING & OVERRIDE
            EditorGUILayout.PropertyField(m_NumberLeftPadding);
            EditorGUILayout.PropertyField(m_OverwriteFiles);


            // FULL NAME PREVIEW
            var firstResolution = m_Config.GetFirstActiveResolution();
            fullName = m_Config.ParseFileName(firstResolution, System.DateTime.Now);

            if (m_Config.m_Cameras.Count > 0 && m_Config.m_Cameras[0] != null && m_Config.m_Cameras[0].m_Camera != null)
            {
                fullName = fullName.Replace("{layer}", m_Config.m_Cameras[0].m_Camera.name);
            }
            if (m_Config.m_Batches.Count > 0 && m_Config.m_Batches[0] != null)
            {
                fullName = fullName.Replace("{batch}", m_Config.m_Batches[0].m_Name);
            }
            if (m_Config.m_Compositions.Count > 0 && m_Config.m_Compositions[0] != null)
            {
                fullName = fullName.Replace("{composer}", m_Config.m_Compositions[0].m_Name);
            }
            fullName = fullName.Replace("{burstFrameCount}", (0).ToString().PadLeft(m_Config.m_NumberLeftPadding, '0'));
            if (!m_Config.m_OverwriteFiles)
            {
                fullName = PathUtils.PreventOverwrite(fullName, m_Config.m_NumberLeftPadding);
            }

            // HELPBOX
            if (m_Config.m_FileName == "" || PathUtils.IsValidPath(m_Config.GetPath()) && !PathUtils.IsValidPath(fullName))
            {
                EditorGUILayout.HelpBox("Name is invalid.", MessageType.Warning);
            }



            // Format			
            //			if (m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW) {
            EditorGUILayout.PropertyField(m_FileFormat);
            if (m_Config.m_FileFormat == TextureExporter.ImageFileFormat.JPG)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Slider(m_JPGQuality, 1f, 100f);
                EditorGUI.indentLevel--;
            }
            else
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_ColorFormat);

                if (m_Config.m_ColorFormat == ScreenshotTaker.ColorFormat.RGBA)
                {
#if USC_UNITY_PIPELINE_HDRP
                    EditorGUILayout.HelpBox("To compute transparent background images with HDRP, please make sure your color buffer format is set to R16G16B16A16 in the render pipeline asset.", MessageType.Warning);
#endif
#if USC_UNITY_PIPELINE_URP || USC_UNITY_PIPELINE_HDRP
                    EditorGUILayout.HelpBox("In case of issues with transparent background, try disabling some HDRP effects, like bloom, tonemapping, and set Visual Environment volume override to None.\n"
                        + "Use RecomputeAlphaLayer to fully recompute the alpha layer."
                    , MessageType.Warning);
                    EditorGUILayout.PropertyField(m_RecomputeAlphaLayer);
#endif

#if USC_UNITY_PIPELINE_HDRP
                    EditorGUILayout.PropertyField(m_DisableHDRPFX);
#endif

                }



                EditorGUI.indentLevel--;
            }
            //			}


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            // Display full name
            EditorGUILayout.LabelField("Full name preview: " + fullName, EditorStyles.miniLabel);
            EditorGUILayout.HelpBox("You can use the following symbols to customize the screenshot names: \n" + ScreenshotNamePresets.m_Presets, MessageType.Info);

        }




        public void DrawShareGUI()
        {

            //Title
            var title = IconsUtils.TryGetIcon("Particle Effect");

            title.text = "Share".ToUpper();
            m_Config.m_ShowShare = EditorGUILayout.Foldout(m_Config.m_ShowShare, title);
            if (m_Config.m_ShowShare == false)
                return;
            EditorGUILayout.Separator();

            var activeResolution = m_Config.m_Resolutions.Count > 0 ? m_Config.m_Resolutions[0] : m_Config.m_GameViewResolution;
            EditorGUILayout.PropertyField(m_ShareSubject);
            EditorGUILayout.LabelField("Full share subject: " + ScreenshotNameParser.ParseSymbols(m_Config.m_ShareSubject, activeResolution, System.DateTime.Now, m_Config.m_NumberLeftPadding), EditorStyles.miniLabel);
            EditorGUILayout.PropertyField(m_ShareText);
            EditorGUILayout.LabelField("Full share text: " + ScreenshotNameParser.ParseSymbols(m_Config.m_ShareText, activeResolution, System.DateTime.Now, m_Config.m_NumberLeftPadding), EditorStyles.miniLabel);

            EditorGUILayout.HelpBox("You can use the following symbols to customize the share subject and text: \n" + ScreenshotNamePresets.m_Presets, MessageType.Info);
        }

        #endregion


        #region CAPTURE

        public void DrawCaptureModeGUI()
        {
            EditorGUILayout.PropertyField(m_CaptureMode);

#if USC_UNITY_PIPELINE_HDRP && !USC_HDRP
            EditorGUILayout.HelpBox("HDRP detected but HDRP support is disabled. Enable it on the install settings section.", MessageType.Warning);
#endif


            if (m_Config.m_CaptureMode == ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING)
            {
                EditorGUILayout.HelpBox("GAMEVIEW_RESIZING is for Editor and Windows Standalone only, can capture the UI, can capture custom resolutions.", MessageType.Info);
            }
            else if (m_Config.m_CaptureMode == ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE)
            {
                EditorGUILayout.HelpBox("RENDER_TO_TEXTURE is for Editor and all platforms, can not capture the UI, can capture custom resolutions.", MessageType.Info);
            }
            else if (m_Config.m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW)
            {
                EditorGUILayout.HelpBox("FIXED_GAMEVIEW is for Editor and all platforms, can capture the UI, can only capture at the screen resolution.", MessageType.Info);
            }
            else if (m_Config.m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL)
            {
                EditorGUILayout.HelpBox("SCENE_VIEW is for Editor only, it captures the Sceneview windows camera content without UI or Gizmos.", MessageType.Info);
            }
            else if (m_Config.m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {
                EditorGUILayout.HelpBox("SCENE_VIEW is for Editor only, it captures the Sceneview windows content extactly as it is, with UI and Gizmos.", MessageType.Info);
            }



            if (m_Config.m_CaptureMode == ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_AntiAliasing);
                EditorGUI.indentLevel--;

                if (m_Config.m_MultisamplingAntiAliasing != ScreenshotConfig.AntiAliasing.NONE)
                {
                    bool incompatibility = false;
                    foreach (ScreenshotCamera camera in m_Config.m_Cameras)
                    {
                        if (camera.m_Camera == null)
                            continue;
#if UNITY_5_6_OR_NEWER
                        if (camera.m_Camera.allowHDR)
                        {
#else
                        if (camera.m_Camera.hdr)
                        {
#endif
                            incompatibility = true;
                        }
                    }
                    if (incompatibility)
                    {
                        EditorGUILayout.HelpBox("It is impossible to use MultiSampling Antialiasing when one or more camera is using HDR.", MessageType.Warning);
                    }
                }

                if (!UnityVersion.HasPro())
                {
                    EditorGUILayout.HelpBox("RENDER_TO_TEXTURE requires Unity Pro or Unity 5.0 and later.", MessageType.Error);
                }
            }

        }

        #endregion

        #region CAMERA

        void CreateCameraReorderableList()
        {
            m_CameraReorderableList = new ReorderableList(serializedObject, m_Cameras, true, true, true, true);
            m_CameraReorderableList.drawElementCallback = (Rect position, int index, bool active, bool focused) =>
            {
                SerializedProperty element = m_CameraReorderableList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(position, element);
            };
            m_CameraReorderableList.drawHeaderCallback = (Rect position) =>
            {
                EditorGUI.LabelField(position, "Active             Camera                                                  Settings");
            };
            m_CameraReorderableList.onAddCallback = (ReorderableList list) =>
            {
                m_Config.m_Cameras.Add(new ScreenshotCamera());
                EditorUtility.SetDirty(m_Obj);
            };
            m_CameraReorderableList.elementHeight = 8 * 20;
        }

        public void DrawCamerasGUI()
        {
            // Title
            var title = IconsUtils.TryGetIcon("Camera Icon");
            title.text = "Cameras".ToUpper();
            m_Config.m_ShowCameras = EditorGUILayout.Foldout(m_Config.m_ShowCameras, title);
            // m_Config.m_ShowCameras = EditorGUILayout.Foldout(m_Config.m_ShowCameras, "Cameras".ToUpper());
            if (m_Config.m_ShowCameras == false)
                return;



            // if (m_Config.m_CaptureMode == ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE)
            // {
            //     m_Config.m_CameraMode = ScreenshotConfig.CamerasMode.CUSTOM_CAMERAS;
            // }
            // else
            // {
            EditorGUILayout.PropertyField(m_CameraMode);
            // }

            if (m_Config.m_CameraMode == ScreenshotConfig.CamerasMode.CUSTOM_CAMERAS)
            {

                EditorGUILayout.PropertyField(m_ExportToDifferentLayers);
                EditorGUILayout.Separator();



                // List
                m_CameraReorderableList.DoLayoutList();

                EditorGUILayout.HelpBox("Note that you only can reference cameras within the scene.", MessageType.Info);



            }

        }


        #endregion

        #region RESOLUTIONS


        void DrawResolution(Rect position, SerializedProperty property)
        {
            Rect activeRect = new Rect(position.x, position.y, 20, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_Active"), GUIContent.none);


            string name = property.FindPropertyRelative("m_ResolutionName").stringValue;
            if (name == "")
            {
                name = "no name";
            }

            activeRect.x += activeRect.width + 0;
            activeRect.width = 300;
            EditorGUI.LabelField(activeRect, name, EditorStyles.boldLabel);
        }



        void DrawResolutionName(Rect position, SerializedProperty property)
        {
            Rect activeRect = new Rect(position.x, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 1, 20, EditorGUIUtility.singleLineHeight);


            activeRect.x += activeRect.width + 0;
            activeRect.width = 75;
            EditorGUI.LabelField(activeRect, "Name");


            activeRect.x += activeRect.width + 2;
            int space = (int)activeRect.x;
            Rect nameRect = new Rect(space, activeRect.y, (position.width + 40 - space) / 2, 18);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("m_ResolutionName"), GUIContent.none);

            Rect categoryRect = new Rect(space + 8 + (position.width + 40 - space) / 2, activeRect.y, (position.width - space) / 2, 18);
            EditorGUI.PropertyField(categoryRect, property.FindPropertyRelative("m_Category"), GUIContent.none);

        }



        void DrawResolutionPlatform(Rect position, SerializedProperty property)
        {
            Rect activeRect = new Rect(position.x, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 2, 20, EditorGUIUtility.singleLineHeight);


            activeRect.x += activeRect.width + 0;
            activeRect.width = 75;
            EditorGUI.LabelField(activeRect, "Platform");

            activeRect.x += activeRect.width + 2;
            activeRect.width = 150;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_Platform"), GUIContent.none);

        }


        void DrawResolutionRes(Rect position, SerializedProperty property)
        {
            Rect activeRect = new Rect(position.x, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 3, 20, EditorGUIUtility.singleLineHeight);

            activeRect.x += activeRect.width + 0;
            activeRect.width = 75;
            EditorGUI.LabelField(activeRect, "Resolution");


            activeRect.x += activeRect.width + 2;
            activeRect.width = 45;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_Width"), GUIContent.none);

            activeRect.x += activeRect.width + 2;
            activeRect.width = 10;
            EditorGUI.PrefixLabel(activeRect, new GUIContent("x"));

            activeRect.x += activeRect.width + 2;
            activeRect.width = 45;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_Height"), GUIContent.none);

            activeRect.x += activeRect.width + 4;
            activeRect.width = 40;
            EditorGUI.LabelField(activeRect, "Scale");

            activeRect.x += activeRect.width + 4;
            activeRect.width = 20;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_Scale"), GUIContent.none);


            activeRect.x += activeRect.width + 4;
            activeRect.width = 50;
            EditorGUI.PrefixLabel(activeRect, new GUIContent(property.FindPropertyRelative("m_Ratio").stringValue));
        }



        void DrawResolutionOrientation(Rect position, SerializedProperty property)
        {

            Rect activeRect = new Rect(position.x + 20, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 4, 20, EditorGUIUtility.singleLineHeight);

            activeRect.width = 75;
            EditorGUI.LabelField(activeRect, "Orientation");

            activeRect.x += activeRect.width + 2;
            activeRect.width = 150;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_Orientation"), GUIContent.none);

        }

        void DrawDeviceResolutionPPI(Rect position, SerializedProperty property)
        {

            Rect activeRect = new Rect(position.x + 20, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 5, 20, EditorGUIUtility.singleLineHeight);

            activeRect.width = 75;
            EditorGUI.LabelField(activeRect, "PPI");

            activeRect.x += activeRect.width + 2;
            activeRect.width = 30;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_PPI"), GUIContent.none);


            activeRect.x += activeRect.width + 4;
            activeRect.width = 75;
            EditorGUI.LabelField(activeRect, "forced PPI");


            activeRect.x += activeRect.width + 4;
            activeRect.width = 40;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_ForcedUnityPPI"), GUIContent.none);



        }

        void DrawDeviceCanvasResolution(Rect position, SerializedProperty property)
        {

            Rect activeRect = new Rect(position.x + 20, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 6, 20, EditorGUIUtility.singleLineHeight);

            activeRect.width = 75;
            EditorGUI.LabelField(activeRect, "Device");

            activeRect.x += activeRect.width + 2;
            activeRect.width = 170;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_DeviceCanvas"), GUIContent.none);

        }



        void DrawDeviceSafeAreaToggle(Rect position, SerializedProperty property)
        {

            Rect activeRect = new Rect(position.x + 20, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 7, 20, EditorGUIUtility.singleLineHeight);

            activeRect.width = 182;
            EditorGUI.LabelField(activeRect, "Disable Safe Area");

            activeRect.x += activeRect.width + 2;
            activeRect.width = 212;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_DisableSafeArea"), GUIContent.none);

            position.y += (EditorGUIUtility.singleLineHeight + 2) * 2;
        }


        void DrawDeviceSafeArea(Rect position, SerializedProperty property)
        {

            Rect activeRect = new Rect(position.x + 20, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 10, 20, EditorGUIUtility.singleLineHeight);

            activeRect.width = 170;
            EditorGUI.LabelField(activeRect, "Safe Area Landscape");

            activeRect.x += activeRect.width + 2;
            activeRect.width = 190;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_SafeAreaLandscapeLeft"), GUIContent.none);

            position.y += (EditorGUIUtility.singleLineHeight + 2) * 2;
        }


        // void DrawDeviceSafeAreaRight(Rect position, SerializedProperty property)
        // {

        //     Rect activeRect = new Rect(position.x + 20, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 11, 20, EditorGUIUtility.singleLineHeight);

        //     activeRect.width = 170;
        //     EditorGUI.LabelField(activeRect, "Safe Area Landscape right");

        //     activeRect.x += activeRect.width + 2;
        //     activeRect.width = 190;
        //     EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_SafeAreaLandscapeRight"), GUIContent.none);

        //     position.y += (EditorGUIUtility.singleLineHeight + 2) * 2;
        // }

        void DrawDeviceSafeAreaPortrait(Rect position, SerializedProperty property)
        {

            Rect activeRect = new Rect(position.x + 20, position.y + 2 + (EditorGUIUtility.singleLineHeight + 2) * 8, 20, EditorGUIUtility.singleLineHeight);

            activeRect.width = 170;
            EditorGUI.LabelField(activeRect, "Safe Area Portrait");

            activeRect.x += activeRect.width + 2;
            activeRect.width = 190;
            EditorGUI.PropertyField(activeRect, property.FindPropertyRelative("m_SafeAreaPortrait"), GUIContent.none);

            position.y += (EditorGUIUtility.singleLineHeight + 2) * 2;
        }

        public void CreateResolutionReorderableList()
        {
            m_ResolutionReorderableList = new ReorderableList(serializedObject, m_Resolutions, true, true, true, true);

            if (m_ShowDetailedDevice && m_Expanded)
            {
                m_ResolutionReorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 15f;
            }
            else
            {
                m_ResolutionReorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 1f;
            }



            m_ResolutionReorderableList.drawElementCallback = (Rect position, int index, bool active, bool focused) =>
            {


                if (m_ShowDetailedDevice)
                {
                    DrawResolution(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                    if (m_Expanded)
                    {
                        DrawResolutionName(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        DrawResolutionPlatform(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));

                        DrawResolutionRes(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        DrawDeviceResolutionPPI(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        DrawResolutionOrientation(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        DrawDeviceCanvasResolution(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        DrawDeviceSafeAreaToggle(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        DrawDeviceSafeAreaPortrait(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        DrawDeviceSafeArea(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        // DrawDeviceSafeAreaRight(position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index));
                        //						DrawDeviceSafeAreaRight (position, m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex (index));


                    }

                }
                else
                {
                    SerializedProperty element = m_ResolutionReorderableList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(position, element);
                }

            };

            m_ResolutionReorderableList.onChangedCallback = (ReorderableList list) =>
            {
                m_Config.UpdateRatios();
                EditorUtility.SetDirty(m_Obj);
            };

            m_ResolutionReorderableList.onSelectCallback = (ReorderableList list) =>
            {
                m_Config.UpdateRatios();
                EditorUtility.SetDirty(m_Obj);
            };

            m_ResolutionReorderableList.drawHeaderCallback = (Rect position) =>
            {
                if (m_ShowDetailedDevice)
                {
                    EditorGUI.LabelField(position, "");
                }
                else
                {
                    EditorGUI.LabelField(position, "Active  Width     Height  Scale Ratio   Orientation        Name");
                }
            };

            // Dirty fix
            m_ResolutionReorderableList.onReorderCallback = (ReorderableList list) =>
            {
                foreach (ScreenshotResolution r in m_Config.m_Resolutions)
                {
                    GameObject.DestroyImmediate(r.m_Texture);
                    r.m_Texture = null;
                }
            };


            m_ResolutionReorderableList.onAddDropdownCallback = (Rect position, ReorderableList list) =>
            {
                var res = new ScreenshotResolution(); ;
                res.m_ResolutionName = "";
                res.m_Category = "";
                res.m_Width = 800;
                res.m_Height = 600;
                m_Config.m_Resolutions.Add(res);
                // DeviceSelectorWindow.Init();
                // DeviceS
                /*
                var menu = new GenericMenu();

                ScreenshotResolutionPresets.Init();

                ConstructResolutionPresetsMenu(menu);

                menu.AddItem(new GUIContent("new"), false, OnResolutionSelectCallback, new ScreenshotResolution());

                menu.ShowAsContext();
                */
            };

        }
        /*
        void ConstructResolutionPresetsMenu(GenericMenu menu)
        {
            foreach (string key in ScreenshotResolutionPresets.m_Categories.Keys)
            {
                menu.AddItem(new GUIContent(key + "/(add all)"), false, OnResolutionSelectAllCallback, ScreenshotResolutionPresets.m_Categories[key]);
            }

            foreach (ScreenshotResolution res in ScreenshotResolutionPresets.m_ResolutionPresets)
            {
                string name = res.m_Category + "/" + res.ToString();
                menu.AddItem(new GUIContent(name), false, OnResolutionSelectCallback, res);
            }
            EditorUtility.SetDirty(m_Obj);
        }

        void OnResolutionSelectAllCallback(object target)
        {
            List<ScreenshotResolution> selection = (List<ScreenshotResolution>)target;
            foreach (ScreenshotResolution res in selection)
            {
                m_Config.m_Resolutions.Add(new ScreenshotResolution(res));
            }
            m_Config.UpdateRatios();
            EditorUtility.SetDirty(m_Obj);
        }

        void OnResolutionSelectCallback(object target)
        {
            ScreenshotResolution selection = (ScreenshotResolution)target;
            m_Config.m_Resolutions.Add(new ScreenshotResolution(selection));
            m_Config.UpdateRatios();
            EditorUtility.SetDirty(m_Obj);
        }
        */

        public virtual void DrawResolutionGUI()
        {
            DrawResolutionTitleGUI();

            if (m_Config.m_ShowResolutions == false)
                return;

            EditorGUILayout.PropertyField(m_ResolutionCaptureMode);
            EditorGUILayout.Separator();

            if (m_Config.m_ResolutionCaptureMode == ScreenshotConfig.ResolutionMode.CUSTOM_RESOLUTIONS)
            {
                DrawResolutionContentGUI();

                if (m_Config.GetActiveResolutions().Count == 0)
                {
                    EditorGUILayout.HelpBox("No active resolutions.", MessageType.Warning);
                }
            }



        }

        public virtual void DrawResolutionTitleGUI()
        {
            // m_Config.m_ShowResolutions = EditorGUILayout.Foldout(m_Config.m_ShowResolutions, "Resolutions".ToUpper());
            var title = IconsUtils.TryGetIcon("LayoutElement Icon");
            title.text = " Resolutions".ToUpper();
            m_Config.m_ShowResolutions = EditorGUILayout.Foldout(m_Config.m_ShowResolutions, title);

        }

        public virtual void DrawResolutionContentGUI()
        {
            if (m_Config.m_ShowResolutions == false)
                return;


            // Buttons
            int width = (int)(EditorGUIUtility.currentViewWidth / 3f);
            EditorGUILayout.BeginHorizontal();
            {
                var title = IconsUtils.TryGetIcon("toggle on");
                title.text = "Enable all".ToUpper();
                if (GUILayout.Button(title, GUILayout.MaxWidth(width)))
                {
                    m_Config.SelectAllResolutions();
                    EditorUtility.SetDirty(m_Obj);
                }
            }
            {
                var title = IconsUtils.TryGetIcon("toggle");
                title.text = "Disable all".ToUpper();
                if (GUILayout.Button(title, GUILayout.MaxWidth(width)))
                {
                    m_Config.ClearAllResolutions();
                    EditorUtility.SetDirty(m_Obj);
                }
            }
            {
                var title = IconsUtils.TryGetIcon("vcs_delete");
                title.text = "Remove all".ToUpper();
                if (GUILayout.Button(title, GUILayout.MaxWidth(width)))
                {
                    m_Config.RemoveAllResolutions();
                    EditorUtility.SetDirty(m_Obj);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                var title = IconsUtils.TryGetIcon("");
                title.text = "Set all Portait".ToUpper();
                if (GUILayout.Button(title, GUILayout.MaxWidth(width)))
                {
                    m_Config.SetAllPortait();
                    EditorUtility.SetDirty(m_Obj);
                }
            }
            {
                var title = IconsUtils.TryGetIcon("");
                title.text = "Set all Landscape".ToUpper();
                if (GUILayout.Button(title, GUILayout.MaxWidth(width)))
                {
                    m_Config.SetAllLandscape();
                    EditorUtility.SetDirty(m_Obj);
                }
            }
            {
                var title = IconsUtils.TryGetIcon("");
                title.text = "Set all Landscape right".ToUpper();
                if (GUILayout.Button(title, GUILayout.MaxWidth(width)))
                {
                    m_Config.SetAllLandscapeRight();
                    EditorUtility.SetDirty(m_Obj);
                }
            }

            EditorGUILayout.EndHorizontal();



            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Export active resolution(s) as cutom preset(s)"))
            {
                ScreenshotResolutionPresets.ExportPresets(m_Config.GetActiveResolutions());
                m_Selector.m_Database.InitPresets();
            }

            if (GUILayout.Button("Save as collection"))
            {
                ScreenshotResolutionPresets.ExportAsCollection(m_Config.GetActiveResolutions());
                m_Selector.m_Database.InitPresets();
            }

            EditorGUILayout.EndHorizontal();



            EditorGUILayout.Space();




            //			if (m_IsDevice) {
            if (m_Expanded)
            {
                m_ResolutionReorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 15f;
            }
            else
            {
                m_ResolutionReorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 1f;
            }
            //					if (GUILayout.Button ("Minimize")) {
            //						m_Expanded = false;
            //					}
            //				} else {
            //					if (GUILayout.Button ("Expand resolution settings")) {
            //						m_ResolutionReorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 10f;
            //						m_Expanded = true;
            //					}
            //				}
            //			}


            // List
            m_ResolutionReorderableList.DoLayoutList();



            m_Selector.OnGUI();


            /*
            EditorGUILayout.HelpBox("Use '+' to add preset(s) to the list. Note that your first click may take a few seconds.", MessageType.Info);


            if (GUILayout.Button("Save active resolution(s) as cutom preset(s)"))
            {
                ScreenshotResolutionPresets.ExportPresets(m_Config.GetActiveResolutions());
            }

            */

        }

        #endregion

        #region OVERLAYS

        void CreateOverlayList()
        {
            m_OverlayReorderableList = new ReorderableList(serializedObject, m_Overlays, true, true, true, true);
            m_OverlayReorderableList.drawElementCallback = (Rect position, int index, bool active, bool focused) =>
            {
                SerializedProperty element = m_OverlayReorderableList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(position, element);
            };
            m_OverlayReorderableList.drawHeaderCallback = (Rect position) =>
            {
                EditorGUI.LabelField(position, "Active     Canvas");
            };
        }

        public void DrawOverlaysGUI()
        {
            // Title
            var title = IconsUtils.TryGetIcon("Canvas Icon");
            title.text = " Overlays".ToUpper();
            m_Config.m_ShowCanvas = EditorGUILayout.Foldout(m_Config.m_ShowCanvas, title);

            // m_Config.m_ShowCanvas = EditorGUILayout.Foldout(m_Config.m_ShowCanvas, "Overlays".ToUpper());
            if (m_Config.m_ShowCanvas == false)
                return;
            EditorGUILayout.Separator();


            // Auto add
            EditorGUILayout.PropertyField(m_CaptureActiveUICanvas);
            EditorGUILayout.PropertyField(m_ForceUICullingLayer);


            // List
            m_OverlayReorderableList.DoLayoutList();

            if (m_Config.m_CaptureActiveUICanvas && m_Config.m_CaptureMode == ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE)
            {
                EditorGUILayout.HelpBox("Note that Screenspace Overlay Canvas and Overlays can not be rendered in RENDER_TO_TEXTURE mode.", MessageType.Info);
            }
            else if (m_Config.m_CaptureActiveUICanvas && m_Config.m_CameraMode == ScreenshotConfig.CamerasMode.CUSTOM_CAMERAS)
            {
                EditorGUILayout.HelpBox("Note that some of your UI will not be rendered if its layer isn't in any active camera culling mask.", MessageType.Info);
            }
        }

        #endregion


        #region COMPOSITION

        public void DrawCompositionGUI()
        {

            var title = IconsUtils.TryGetIcon("PlayableDirector Icon");
            title.text = " Composition".ToUpper();

            m_Config.m_ShowComposition = EditorGUILayout.Foldout(m_Config.m_ShowComposition, title);
            // m_Config.m_ShowComposition = EditorGUILayout.Foldout(m_Config.m_ShowComposition, "Composition".ToUpper());
            if (m_Config.m_ShowComposition == false)
                return;


            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Config.m_CompositionMode"));

            if (m_Config.m_CompositionMode == ScreenshotConfig.CompositionMode.COMPOSITION)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Config.m_Compositions"), true);
            }




        }

        public void DrawBatchesGUI()
        {

            var title = IconsUtils.TryGetIcon("EventSystem Icon");
            title.text = " Batches".ToUpper();

            m_Config.m_ShowBatches = EditorGUILayout.Foldout(m_Config.m_ShowBatches, title);
            // m_Config.m_ShowBatches = EditorGUILayout.Foldout(m_Config.m_ShowBatches, "Batches".ToUpper());
            if (m_Config.m_ShowBatches == false)
                return;

            m_Config.UpdateBatchesData();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Config.m_BatchMode"));

            if (m_Config.m_BatchMode == ScreenshotConfig.BatchMode.BATCHES)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Config.m_Batches"), true);
            }

        }


        #endregion

        #region PREVIEW


        float m_PreviewWidth;
        float m_PreviewHeight;

        public void DrawPreviewGUI()
        {

            // Title
            var title = IconsUtils.TryGetIcon("Texture Icon");
            title.text = " Preview".ToUpper();
            m_Config.m_ShowPreviewGUI = EditorGUILayout.Foldout(m_Config.m_ShowPreviewGUI, title);
            // m_Config.m_ShowPreviewGUI = EditorGUILayout.Foldout(m_Config.m_ShowPreviewGUI, "Preview".ToUpper());
            if (m_Config.m_ShowPreviewGUI == false)
                return;
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(m_PreviewInGameViewWhilePlaying);
            if (m_Config.m_PreviewInGameViewWhilePlaying)
            {
                EditorGUILayout.HelpBox("Preview in game while playing is enabled. This will temporary overwrite your camera settings and generate overlays canvas when you start running the game in editor, to allow you to play while the gameview use your screenshot custom settings.", MessageType.Warning);
            }
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();


            EditorGUILayout.PropertyField(m_ShowGuidesInPreview);
            EditorGUILayout.PropertyField(m_GuideCanvas);
            EditorGUILayout.PropertyField(m_GuidesColor);


            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(m_ShowPreview);


            if (m_Config.m_ShowPreview)
            {

                EditorGUILayout.Slider(m_PreviewSize, 0.05f, 1f);

                // Draw preview texture if any
                if (m_Config.GetFirstActiveResolution().m_Texture != null)
                {

                    // On repaint event, compute the preview dimensions and update the texture if needed
#if UNITY_2017_3_OR_NEWER
                    if (Event.current.type == EventType.Repaint)
                    {
#else
                    if (Event.current.type == EventType.repaint)
                    {
#endif

                        if (m_Config.GetFirstActiveResolution().IsValid())
                        {

                            m_PreviewWidth = m_Config.m_PreviewSize * GUILayoutUtility.GetLastRect().width;
                            m_PreviewHeight = m_PreviewWidth * m_Config.GetFirstActiveResolution().m_Texture.height / m_Config.GetFirstActiveResolution().m_Texture.width;

                        }
                    }


                    // Draw an empty label to make some place to display the preview texture
                    EditorGUILayout.LabelField("", GUILayout.Height(m_PreviewHeight));

                    Rect previewRect = GUILayoutUtility.GetLastRect();
                    previewRect.x = previewRect.x + previewRect.width / 2 - m_PreviewWidth / 2;
                    previewRect.width = m_PreviewWidth;
                    previewRect.height = m_PreviewHeight;
                    EditorGUI.DrawPreviewTexture(previewRect, m_Config.GetFirstActiveResolution().m_Texture);
                }
                else
                {
                    EditorGUILayout.Separator();
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Press update to create the preview image.", UIStyle.centeredGreyTextStyle);
                    EditorGUILayout.Separator();
                }

            }

        }

        #endregion

        #region CAPTURE



        public virtual void DrawCaptureGUI()
        {
            // Title
            m_Config.m_ShowCapture = EditorGUILayout.Foldout(m_Config.m_ShowCapture, "Capture".ToUpper());
            if (m_Config.m_ShowCapture == false)
                return;
            EditorGUILayout.Separator();

            // Mode selection
            EditorGUILayout.PropertyField(m_ShotMode);
            if (m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_MaxBurstShotsNumber);
                EditorGUILayout.PropertyField(m_MaxShotPerSeconds);
                EditorGUILayout.PropertyField(m_FixedFrameRate);

                EditorGUI.indentLevel--;
            }

            // Info message

            if (m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST && !Application.isPlaying)
            {
                EditorGUILayout.HelpBox("The application needs to be playing to take the screenshots.", MessageType.Info);
            }

            if (m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST
                && m_Config.m_PreviewInGameViewWhilePlaying && m_Config.GetActiveResolutions().Count > 1)
            {
                EditorGUILayout.HelpBox("In burst mode and PreviewInGameViewWhilePlaying mode, it is recommended to only capture one resolution at a time to prevent GameView deformations while capturing.", MessageType.Warning);
            }

            // Warning message
            if (m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST
                && m_Config.m_OverwriteFiles)
            {
                EditorGUILayout.HelpBox("The file overwrite mode is enabled: burst screenshots are probably going to be overwritten. Set overwrite to false to automatically increment screenshot names.", MessageType.Warning);

            }


        }

        #endregion


        #region UTILS

        public virtual void DrawHotkeysGUI()
        {
            // Title
            var title = IconsUtils.TryGetIcon("StandaloneInputModule Icon");
            title.text = " Hotkeys".ToUpper();
            m_Config.m_ShowHotkeys = EditorGUILayout.Foldout(m_Config.m_ShowHotkeys, title);
            // m_Config.m_ShowHotkeys = EditorGUILayout.Foldout(m_Config.m_ShowHotkeys, "Hotkeys.".ToUpper());
            if (m_Config.m_ShowHotkeys == false)
                return;
            EditorGUILayout.Separator();


#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            // EditorGUILayout.LabelField("New InputSystem hotkeys");
            DrawHotkey("Capture Key", m_Config.m_CaptureHotkey, true);
            DrawHotkey("Align To View Key", m_Config.m_AlignHotkey, true);
            DrawHotkey("Update Preview Key", m_Config.m_UpdatePreviewHotkey, true);
            DrawHotkey("Pause Key (ingame only)", m_Config.m_PauseHotkey, true);
#else
            DrawHotkey("Capture Key", m_Config.m_CaptureHotkey);
            DrawHotkey("Align To View Key", m_Config.m_AlignHotkey);
            DrawHotkey("Update Preview Key", m_Config.m_UpdatePreviewHotkey);
            DrawHotkey("Pause Key (ingame only)", m_Config.m_PauseHotkey);
#endif


            EditorGUILayout.HelpBox("Note that these hotkeys only work in Playing mode, or in Edit mode when focused on the SceneView, on the inspector or on the editor window.", MessageType.Info);

        }

        public void DrawUtilsGUI()
        {
            // Title
            var title = IconsUtils.TryGetIcon("EditorSettings Icon");
            title.text = " Utils".ToUpper();
            m_Config.m_ShowUtils = EditorGUILayout.Foldout(m_Config.m_ShowUtils, title);
            // m_Config.m_ShowUtils = EditorGUILayout.Foldout(m_Config.m_ShowUtils, "Utils".ToUpper());
            if (m_Config.m_ShowUtils == false)
                return;
            EditorGUILayout.Separator();

            // Destroy
            EditorGUILayout.PropertyField(m_DontDestroyOnLoad);
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();


            // Time
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Time scale");
            float timeScale = m_Config.m_Time;
            float time = EditorGUILayout.Slider(timeScale, 0f, 1f);
            if (time != timeScale)
            {
                m_Config.SetTime(time);
            }
            EditorGUILayout.EndHorizontal();

            if (time == 0f)
            {
                EditorGUILayout.HelpBox("Time scale is set to 0.", MessageType.Warning);
            }

            // Pause button
            if (Time.timeScale == 0f)
            {
                if (GUILayout.Button("Resume game (set time scale to 1)"))
                {
                    m_Config.TogglePause();
                }
            }
            else
            {
                if (GUILayout.Button("Pause game (set time scale to 0)"))
                {
                    m_Config.TogglePause();
                }
            }

            // Align
            if (GUILayout.Button("Align cameras to view"))
            {
                m_Config.AlignToView();
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            // Sounds
            EditorGUILayout.PropertyField(m_PlaySoundOnCapture);
            EditorGUILayout.PropertyField(m_ShotSound);
            EditorGUILayout.PropertyField(m_StopTimeOnCapture);

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

        }


        // bool permissionChecked = false;
        PhotoUsageDescription usage = null;

        public void DrawUsage()
        {
            // Title
            var title = IconsUtils.TryGetIcon("Clipboard");
            title.text = " Permissions".ToUpper();
            m_Config.m_ShowUsage = EditorGUILayout.Foldout(m_Config.m_ShowUsage, title);
            // m_Config.m_ShowUsage = EditorGUILayout.Foldout(m_Config.m_ShowUsage, "Permissions".ToUpper());
            if (m_Config.m_ShowUsage == false)
                return;

            if (usage == null)
            {
                usage = AssetUtils.GetOrCreate<PhotoUsageDescription>("PhotoUsageDescription", "Assets/AlmostEngine/UltimateScreenshotCreator/Assets/Editor/");
            }
            if (usage == null)
                return;

            /*
			// Exlude from build
			EditorGUILayout.Separator ();
			EditorGUILayout.LabelField ("Editor Only");
			if (!usage.m_EditorOnly) {
				EditorGUILayout.HelpBox ("If you want to use the asset in editor only, use the button below to exclude it from build. " +
				"Be careful not to use any scripts in builds to prevent errors.", MessageType.Info);
				if (GUILayout.Button ("Switch to editor only")) {
					ExcludeFromBuild.Exclude (true);
					usage.m_EditorOnly = true;
				}
			} else {
				EditorGUILayout.HelpBox ("The asset is currently in Editor Only mode. Use the button below to restore the asset in build mode.", MessageType.Info);
				if (GUILayout.Button ("Switch to build mode")) {
					ExcludeFromBuild.Exclude (false);
					usage.m_EditorOnly = false;
				}
			}
			*/

            // Add framework dependency to iOS
            // if (!permissionChecked)
            // {
            //     permissionChecked = true;
            //     FrameworkDependency.SetiOSFrameworkDependency();
            // }



            // Exclude from iOS and Android
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            bool neediOSGalleryPermission = !RemovePermissionNeeds.IsSymbolDefined(BuildTargetGroup.iOS, "USC_EXCLUDE_IOS_GALLERY");


            // EditorGUILayout.LabelField("iOS Gallery permission: " + neediOSGalleryPermission);
            EditorGUILayout.HelpBox("iOS Gallery permission is required to add screenshots to the gallery.\n" +
            "If you don't need to access to the device gallery, you can disable the permission. ", MessageType.Info);
            if (neediOSGalleryPermission)
            {
                if (GUILayout.Button("iOS Gallery permission : Enabled"))
                {
                    RemovePermissionNeeds.ToggleiOSGalleryPermission(false);
                    neediOSGalleryPermission = false;
                }
                EditorGUILayout.LabelField("iOS Photo library usage description:");
                string newUsage = EditorGUILayout.TextArea(usage.m_UsageDescription, EditorStyles.textArea);
                if (newUsage != usage.m_UsageDescription)
                {
                    usage.m_UsageDescription = newUsage;
                    EditorUtility.SetDirty(usage);
                }


            }
            else
            {
                var colorBk = GUI.color;
                GUI.color = Color.gray;
                if (GUILayout.Button("iOS Gallery permission : Disabled"))
                {
                    RemovePermissionNeeds.ToggleiOSGalleryPermission(true);
                    neediOSGalleryPermission = true;
                }
                GUI.color = colorBk;
            }



            // EditorGUILayout.LabelField("Android storage permission: " + UnityEditor.PlayerSettings.Android.forceSDCardPermission);

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.HelpBox("Android storage permission is required if you want to export to the Android Pictures folder.\n" +
            "If you do not require to access to the Pictures folder, you can disable the permission below. ", MessageType.Info);

            if (UnityEditor.PlayerSettings.Android.forceSDCardPermission)
            {
                if (GUILayout.Button("Android storage permission : Enabled"))
                {
                    RemovePermissionNeeds.RemoveAndroidStoragePermission(true);
                }
            }
            else
            {
                var colorBk = GUI.color;
                GUI.color = Color.gray;
                if (GUILayout.Button("Android storage permission : Disabled"))
                {
                    RemovePermissionNeeds.RemoveAndroidStoragePermission(false);
                }
                GUI.color = colorBk;
            }


            EditorGUILayout.HelpBox("Android legacy external storage is required if you want to export to the Android Pictures folder on Android 10 (SDK 29).\n" +
            "If disabled, on Android 10 (SDK 29) the pictures will be saved in the app media folder.\n" +
            "This is not required for any other Android version.", MessageType.Info);

            if (RemovePermissionNeeds.IsSymbolDefined(BuildTargetGroup.Android, "USC_ANDROID_LEGACY_EXTERNAL_STORAGE"))
            {
                if (GUILayout.Button("Android legacy external storage : Enabled"))
                {
                    RemovePermissionNeeds.AndroidLegacyExternalStorage(false);
                }
            }
            else
            {
                var colorBkb = GUI.color;
                GUI.color = Color.gray;
                if (GUILayout.Button("Android legacy external storage : Disabled"))
                {
                    RemovePermissionNeeds.AndroidLegacyExternalStorage(true);
                }
                GUI.color = colorBkb;
            }
        }

        public void DrawFeatureExclude()
        {
            // Title
            var title = IconsUtils.TryGetIcon("VerticalLayoutGroup Icon");
            title.text = " Install mode".ToUpper();
            m_Config.m_ShowInstall = EditorGUILayout.Foldout(m_Config.m_ShowInstall, title);
            // m_Config.m_ShowInstall = EditorGUILayout.Foldout(m_Config.m_ShowInstall, "Install mode".ToUpper());
            if (m_Config.m_ShowInstall == false)
                return;

            if (usage == null)
            {
                usage = AssetUtils.GetOrCreate<PhotoUsageDescription>("PhotoUsageDescription", "Assets/AlmostEngine/UltimateScreenshotCreator/Assets/Editor/");
            }
            if (usage == null)
                return;

            if (ExcludeFromBuild.IsEnabled())
            {
                if (GUILayout.Button("Exclude from build is Enabled"))
                {
                    ExcludeFromBuild.SetEnabled(false);
                }
            }
            else
            {
                var colorBk = GUI.color;
                GUI.color = Color.gray;
                if (GUILayout.Button("Exclude from build is Disabled - click to enable"))
                {
                    ExcludeFromBuild.SetEnabled(true);
                }
                GUI.color = colorBk;
            }

            // Default exclusion of native share
            if (!usage.m_FeaturePhoneShare && ((SymbolsUtils.GetCurrentTargetGroup() == BuildTargetGroup.Android && !SymbolsUtils.IsSymbolDefined(BuildTargetGroup.Android, "USC_EXCLUDE_SHARE"))
                                               || (SymbolsUtils.GetCurrentTargetGroup() == BuildTargetGroup.iOS && !SymbolsUtils.IsSymbolDefined(BuildTargetGroup.iOS, "USC_EXCLUDE_SHARE")))
                )
            {
                usage.ToggleShare(false);
            }

            // if (ExcludeFromBuild.IsEnabled())
            {

                EditorGUILayout.HelpBox("Enable or disable the components to be included in the build, depending on your needs. \n"
                                        + "Exclude all for an Editor-only use, no feature will be available in build.", MessageType.Info);
                if (typeof(ScreenshotManager).Assembly.GetType("AlmostEngine.Preview.UniversalDevicePreview") != null)
                {
                    DrawFeatureToggle("Safe Area component", "", ref usage.m_FeatureSafeAreaComponent, usage.ToggleSafeArea);
                }
                DrawFeatureToggle("Capture screenshots in build", "", ref usage.m_FeatureScreenshotCapture, usage.ToggleScreenshot);
                DrawFeatureToggle("Add screenshots to phone Gallery (iOS, Android)", "", ref usage.m_FeaturePhoneGallery, usage.ToggleGallery);
                DrawFeatureToggle("Share screenshots (iOS, Android, WebGL)", "", ref usage.m_FeaturePhoneShare, usage.ToggleShare);
                if (SymbolsUtils.GetCurrentTargetGroup() == BuildTargetGroup.Android && (!ExcludeFromBuild.IsEnabled() || usage.m_FeaturePhoneShare))
                {
                    EditorGUILayout.HelpBox("NativeShare dependency requires building with gradle 5.6.4. or newer. In case of Gradle build failure, exclude Share feature from the project or update Gradle version.", MessageType.Warning);
                    if ((int)PlayerSettings.Android.minSdkVersion < 31)
                    {
                        EditorGUILayout.HelpBox("Nativeshare dependency requires PlayerSettings.Android.targetSdkVersion to 31 or higher. Current version is " + PlayerSettings.Android.minSdkVersion + ".", MessageType.Error);
                    }
                }
                DrawFeatureToggle("Simple Localization", "", ref usage.m_FeatureSimpleLocalization, usage.ToggleLocalization);
                DrawFeatureToggle("Examples", "", ref usage.m_FeatureExamples, usage.ToggleExamples);

            }


            var colorBBk = GUI.color;
#if ENABLE_INPUT_SYSTEM && UNITY_2018_4_OR_NEWER
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("New Input System support");
#if !USC_INPUT_SYSTEM
            GUI.color = Color.gray;
            if (GUILayout.Button("InputSystem support is disabled")) { InputSystemSupport.ToggleInputSystemSupport(true); }
            GUI.color = colorBBk;
#else
            if (GUILayout.Button("InputSystem support is enabled")) { InputSystemSupport.ToggleInputSystemSupport(false); }
#endif
#endif



#if USC_UNITY_PIPELINE_HDRP
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("HDRP support");
#if !USC_HDRP
            GUI.color = Color.gray;
            if (GUILayout.Button("HDRP support is disabled")) { HDRPSupport.ToggleHDRPSupport(true); }
            GUI.color = colorBBk;
#else
            if (GUILayout.Button("HDRP support is enabled")) { HDRPSupport.ToggleHDRPSupport(false); }
#endif
#endif




        }

        void DrawFeatureToggle(string name, string description, ref bool enabled, UnityAction<bool> onToggle)
        {
            var colorBk = GUI.color;
            if (enabled)
            {
                GUI.color = Color.white;
            }
            else
            {
                GUI.color = Color.gray;
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(name + (enabled ? " : NOT excluded " : " : Excluded")))
            {
                if (onToggle != null)
                {
                    onToggle(!enabled);
                }
            }

            // EditorGUILayout.HelpBox(description, MessageType.Info);
            EditorGUILayout.EndHorizontal();
            GUI.color = colorBk;
        }

        public void DrawHotkey(string name, HotKey key, bool newInput = false)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(name);

            bool shift = EditorGUILayout.ToggleLeft("Shift", key.m_Shift, GUILayout.MaxWidth(45));
            if (shift != key.m_Shift)
            {
                EditorUtility.SetDirty(m_Obj);
                key.m_Shift = shift;
            }

            bool control = EditorGUILayout.ToggleLeft("Control", key.m_Control, GUILayout.MaxWidth(60));
            if (control != key.m_Control)
            {
                EditorUtility.SetDirty(m_Obj);
                key.m_Control = control;
            }

            bool alt = EditorGUILayout.ToggleLeft("Alt", key.m_Alt, GUILayout.MaxWidth(40));
            if (alt != key.m_Alt)
            {
                EditorUtility.SetDirty(m_Obj);
                key.m_Alt = alt;
            }

#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            if (newInput)
            {
                UnityEngine.InputSystem.Key k = (UnityEngine.InputSystem.Key)EditorGUILayout.EnumPopup(key.m_NewInputKey);
                if (k != key.m_NewInputKey)
                {
                    EditorUtility.SetDirty(m_Obj);
                    key.m_NewInputKey = k;
                }
            }
            else
            {
            if (!newInput)
            {
#endif
            KeyCode k = (KeyCode)EditorGUILayout.EnumPopup(key.m_Key);
            if (k != key.m_Key)
            {
                EditorUtility.SetDirty(m_Obj);
                key.m_Key = k;
            }

#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            }
            }
#endif


            EditorGUILayout.EndHorizontal();


        }

        public void DrawDelay(bool detailHelpbox = true)
        {
            // Title
            var title = IconsUtils.TryGetIcon("UnityEditor.ProfilerWindow");
            title.text = " Capture delay".ToUpper();
            m_Config.m_ShowDelay = EditorGUILayout.Foldout(m_Config.m_ShowDelay, title);
            // m_Config.m_ShowDelay = EditorGUILayout.Foldout(m_Config.m_ShowDelay, "Capture delay".ToUpper());
            if (m_Config.m_ShowDelay == false)
                return;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Config.m_GameViewResizingWaitingMode"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Config.m_ResizingWaitingTime"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Config.m_ResizingWaitingFrames"));

            string help = "If your game contains elements that need several frames or seconds to be updated, like custom game UI or post effects, set the delay to capture them properly.";
            if (detailHelpbox)
            {
                help += " For GameViewResizing capture mode only.";
            }
            EditorGUILayout.HelpBox(help, MessageType.Info);
        }


        #endregion





    }
}
