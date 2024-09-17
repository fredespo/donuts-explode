using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AlmostEngine.Screenshot
{
		[CustomPropertyDrawer (typeof(ScreenshotResolution))]
		/// <summary>
		/// Resolution drawer is used to daw the ScreenshotResolution GUI.
		/// We need to use a CustomPropertyDrawer because we use a Reorderable List of cameras.
		/// </summary>
		public class ResolutionDrawer : PropertyDrawer
		{

				override public void OnGUI (Rect position, SerializedProperty property, GUIContent label)
				{
						EditorGUI.BeginProperty (position, label, property);

						Rect activeRect = new Rect (position.x, position.y, 20, EditorGUIUtility.singleLineHeight);
						EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_Active"), GUIContent.none);

						activeRect.x += activeRect.width + 2;
						activeRect.width = 45;
						EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_Width"), GUIContent.none);
			
						activeRect.x += activeRect.width + 2;
						activeRect.width = 10;
						EditorGUI.PrefixLabel (activeRect, new GUIContent ("x"));
			
						activeRect.x += activeRect.width + 2;
						activeRect.width = 45;
						EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_Height"), GUIContent.none);
			
						activeRect.x += activeRect.width + 4;
						activeRect.width = 20;
						EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_Scale"), GUIContent.none);
			
			
						activeRect.x += activeRect.width + 4;
						activeRect.width = 30;
						EditorGUI.PrefixLabel (activeRect, new GUIContent (property.FindPropertyRelative ("m_Ratio").stringValue));
			
						activeRect.x += activeRect.width + 8;
						activeRect.width = 90;
						EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_Orientation"), GUIContent.none);


			//						if (typeof(ScreenshotManager).Assembly.GetType ("AlmostEngine.Preview.UniversalDevicePreview") != null) {
//
//								activeRect.x += activeRect.width + 4;
//								activeRect.width = 30;
//								EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_PPI"), GUIContent.none);
//				
//								activeRect.x += activeRect.width + 8;
//								activeRect.width = 40;
//								EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_ForcedUnityPPI"), GUIContent.none);
//				
////								activeRect.x += activeRect.width + 8;
////								activeRect.width = 35;
////								EditorGUI.PropertyField (activeRect, property.FindPropertyRelative ("m_Stats"), GUIContent.none);
//
//						}			
			
						activeRect.x += activeRect.width + 8;
						int space = (int)activeRect.x;
						Rect nameRect = new Rect (space, position.y, (position.width + 40 - space) / 2, 18);
						EditorGUI.PropertyField (nameRect, property.FindPropertyRelative ("m_ResolutionName"), GUIContent.none);
			
						Rect categoryRect = new Rect (space + 8 + (position.width + 40 - space) / 2, position.y, (position.width - space) / 2, 18);
						EditorGUI.PropertyField (categoryRect, property.FindPropertyRelative ("m_Category"), GUIContent.none);


						EditorGUI.EndProperty ();

				}
		}
}