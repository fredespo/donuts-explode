using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


namespace AlmostEngine.Screenshot
{
		[CustomPropertyDrawer (typeof(ScreenshotCamera))]
		/// <summary>
		/// Camera drawer is used to daw the ScreenshotCamera GUI.
		/// We need to use a CustomPropertyDrawer because we use a Reorderable List of cameras.
		/// </summary>
		public class CameraDrawer : PropertyDrawer
		{
				override public void OnGUI (Rect position, SerializedProperty property, GUIContent label)
				{
						EditorGUI.BeginProperty (position, label, property);

						// Active
						Rect activeRect = new Rect (position.x, position.y, 20, EditorGUIUtility.singleLineHeight);
						EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_Active"), GUIContent.none);

						// Camera
						Rect cameraRect = new Rect (position.x + 25, position.y, 250, EditorGUIUtility.singleLineHeight);
						EditorGUI.PropertyField (cameraRect, property.FindPropertyRelative ("m_Camera"), GUIContent.none);

						// Settings
						int height = 1;

						// CLEAR
						Rect settingRect = new Rect (position.x + 25, position.y + (height * 20), position.width - 25, EditorGUIUtility.singleLineHeight);
						EditorGUI.PropertyField (settingRect, property.FindPropertyRelative ("m_ClearSettings"));
						height++;

						if (property.FindPropertyRelative ("m_ClearSettings").enumValueIndex != 0) {
								Rect flagsRect = new Rect (position.x + 40, position.y + (height * 20), position.width - 40, EditorGUIUtility.singleLineHeight);
								EditorGUI.PropertyField (flagsRect, property.FindPropertyRelative ("m_ClearFlags"));
								height++;

								Rect colorRect = new Rect (position.x + 40, position.y + (height * 20), position.width - 40, EditorGUIUtility.singleLineHeight);
								EditorGUI.PropertyField (colorRect, property.FindPropertyRelative ("m_BackgroundColor"));
								height++;
						}

						// CULLING
						Rect cullingSettingRect = new Rect (position.x + 25, position.y + (height * 20), position.width - 25, EditorGUIUtility.singleLineHeight);
						EditorGUI.PropertyField (cullingSettingRect, property.FindPropertyRelative ("m_CullingSettings"));
						height++;

						if (property.FindPropertyRelative ("m_CullingSettings").enumValueIndex != 0) {

								// Create culling mask list
								List<string> masks = new List<string> ();
								for (int i = 0; i < 32; ++i) {
										string layer = LayerMask.LayerToName (i);
										if (layer != "") {
												masks.Add (layer);
										} else {
												masks.Add ("no name");
										}
								}

								Rect cullingLabelRect = new Rect (position.x + 40, position.y + (height * 20), (position.width - 40) / 2, EditorGUIUtility.singleLineHeight);
								EditorGUI.LabelField (cullingLabelRect, "Culling Mask");

								Rect cullingRect = new Rect (position.x + 40 + (position.width - 40) / 2, position.y + (height * 20), (position.width - 40) / 2, EditorGUIUtility.singleLineHeight);
								property.FindPropertyRelative ("m_CullingMask").intValue = EditorGUI.MaskField (cullingRect, property.FindPropertyRelative ("m_CullingMask").intValue, masks.ToArray ());
								height++;
						}


						// FOV
						Rect fovSettingRect = new Rect (position.x + 25, position.y + (height * 20), position.width - 25, EditorGUIUtility.singleLineHeight);
						EditorGUI.PropertyField (fovSettingRect, property.FindPropertyRelative ("m_FOVSettings"));
						height++;

						if (property.FindPropertyRelative ("m_FOVSettings").enumValueIndex != 0) {

								Rect colorRect = new Rect (position.x + 40, position.y + (height * 20), position.width - 40, EditorGUIUtility.singleLineHeight);
								EditorGUI.Slider (colorRect, property.FindPropertyRelative ("m_FOV"), 1f, 130);
								height++;
						}

						EditorGUI.EndProperty ();
				}
		}
}