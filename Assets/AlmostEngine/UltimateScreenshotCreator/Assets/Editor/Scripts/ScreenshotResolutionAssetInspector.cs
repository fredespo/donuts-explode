using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;


namespace AlmostEngine.Screenshot
{
    [CustomEditor(typeof(ScreenshotResolutionAsset))]
    [CanEditMultipleObjects]
    public class ScreenshotResolutionAssetInspectorInspector : Editor
    {


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Resolution", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_Width"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_Height"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_Scale"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_Orientation"));



            if (typeof(ScreenshotManager).Assembly.GetType("AlmostEngine.Preview.UniversalDevicePreview") != null)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Device info", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_PPI"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_ForcedUnityPPI"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_Platform"));


                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Embedded device image", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_DeviceCanvas"));

                // Safe Area
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Safe Area", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_SafeAreaPortrait"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Resolution.m_SafeAreaLandscapeLeft"));
            }


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Edit tags", EditorStyles.boldLabel);
            TagGUI();

            serializedObject.ApplyModifiedProperties();
        }


        string newTag = "";
        public void TagGUI()
        {
            var selectedPresets = serializedObject.targetObjects.Cast<ScreenshotResolutionAsset>().ToList();

            // Create tag
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Create tag", GUILayout.MaxWidth(100));
            newTag = EditorGUILayout.TextField(newTag);
            if (GUILayout.Button("Add", GUILayout.MaxWidth(40)))
            {
                foreach (var selected in selectedPresets)
                {
                    TagDatabaseAsset.AddTag((ScreenshotResolutionAsset)selected, newTag);
                }
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.Separator();

            // Existing tags
            var tags = TagDatabaseAsset.GetAllTags();
            foreach (var tag in tags)
            {
                bool hasTag = TagDatabaseAsset.HasTag(selectedPresets, tag);

                EditorGUILayout.BeginHorizontal();
                if (!hasTag)
                {
                    if (GUILayout.Button("+", GUILayout.MaxWidth(20)))
                    {
                        foreach (var preset in selectedPresets)
                        {
                            TagDatabaseAsset.AddTag(preset, tag);
                        }
                    }
                    EditorGUILayout.LabelField(tag, EditorStyles.miniLabel);
                }
                else
                {
                    if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                    {
                        foreach (var preset in selectedPresets)
                        {
                            TagDatabaseAsset.RemoveTag(preset, tag);
                        }
                    }
                    EditorGUILayout.LabelField(tag, EditorStyles.boldLabel);
                }
                EditorGUILayout.EndHorizontal();
            }

        }
    }
}
