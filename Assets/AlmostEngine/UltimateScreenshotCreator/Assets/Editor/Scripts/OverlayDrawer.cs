using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AlmostEngine.Screenshot
{
		[CustomPropertyDrawer (typeof(ScreenshotOverlay))]
		/// <summary>
		/// Overlay drawer is used to daw the ScreenshotOverlay GUI.
		/// We need to use a CustomPropertyDrawer because we use a Reorderable List of cameras.
		/// </summary>
		public class OverlayDrawer : PropertyDrawer
		{
				override public void OnGUI (Rect position, SerializedProperty property, GUIContent label)
				{
						EditorGUI.BeginProperty (position, label, property);

						Rect activeRect = new Rect (position.x, position.y, 20, position.height);
						EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_Active"), GUIContent.none);

						Rect canvasRect = new Rect (position.x + 25, position.y, 250, EditorGUIUtility.singleLineHeight);
						EditorGUI.PropertyField (canvasRect, property.FindPropertyRelative ("m_Canvas"), GUIContent.none);

						EditorGUI.EndProperty ();
				}
		}
}