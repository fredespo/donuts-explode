using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using AlmostEngine.Screenshot;

namespace AlmostEngine.Preview
{
	[InitializeOnLoad]
	public class PPISimulation
	{
		static PPISimulation ()
		{
			ResolutionWindowBase.onUpdateBeginDelegate -= FindAllCanvasScalers;
			ResolutionWindowBase.onUpdateBeginDelegate += FindAllCanvasScalers;
			
			ScreenshotTaker.onResolutionUpdateStartDelegate -= ForceCanvasScales;
			ScreenshotTaker.onResolutionUpdateStartDelegate += ForceCanvasScales;

			ResolutionWindowBase.onUpdateEndDelegate -= RestoreCanvasScalers;
			ResolutionWindowBase.onUpdateEndDelegate += RestoreCanvasScalers;
		}

		static List<CanvasScaler> m_CanvasScalers = new List<CanvasScaler> ();

		#if UNITY_2018_OR_NEWER
		static Dictionary<RectTransform, Vector3> m_Transforms = new Dictionary<RectTransform, Vector3> ();
//		static Dictionary<Canvas, float> m_ScaleFactor = new Dictionary<Canvas, float> ();
//		static Dictionary<Canvas, float> m_PPUFactor = new Dictionary<Canvas, float> ();
		#endif

		static void FindAllCanvasScalers ()
		{

			m_CanvasScalers.Clear ();
			
			CanvasScaler[] scalers = GameObject.FindObjectsOfType<CanvasScaler> ();
			foreach (CanvasScaler scaler in scalers) {
				m_CanvasScalers.Add (scaler);
//				m_ScaleFactor [scaler.GetComponent<Canvas> ()] = scaler.GetComponent<Canvas> ().scaleFactor;
//				m_PPUFactor [scaler.GetComponent<Canvas> ()] = scaler.GetComponent<Canvas> ().referencePixelsPerUnit;
			}

			#if UNITY_2018_OR_NEWER
			RectTransform[] transforms = GameObject.FindObjectsOfType<RectTransform> ();
			foreach (RectTransform t in transforms) {
				if (t.GetComponentInParent<CanvasScaler> () != null && t.GetComponentInParent<CanvasScaler> ().uiScaleMode == CanvasScaler.ScaleMode.ConstantPhysicalSize) {
					m_Transforms [t] = t.anchoredPosition3D;
				}
			}
			#endif
		}

		static void RestoreCanvasScalers ()
		{		
//			foreach (CanvasScaler scaler in m_CanvasScalers) {
//				scaler.GetComponent<Canvas> ().scaleFactor = m_ScaleFactor [scaler.GetComponent<Canvas> ()];
//				scaler.GetComponent<Canvas> ().referencePixelsPerUnit = m_PPUFactor [scaler.GetComponent<Canvas> ()];
//			}
			//
			#if UNITY_2018_OR_NEWER
			foreach (RectTransform t in m_Transforms.Keys) {
				if (t != null) {
					t.anchoredPosition3D = m_Transforms [t];
				}
			}
			#endif
			if (!Application.isPlaying) {
				ForceCanvasScales (Screen.dpi);
			}
			Canvas.ForceUpdateCanvases ();
		}

		/// <summary>
		/// We force the ConstantPhysicalSize Canvas Scaler
		/// to recompute their scale using a dpi different that the Screen.dpi
		/// </summary>
		static void ForceCanvasScales (ScreenshotResolution res)
		{
			if (res.m_PPI <= 0)
				return;

			float dpi;

			if (res.m_ForcedUnityPPI > 0) {
				// If the PPI returned by the Device to Unity by Screen.dpi is bad,
				// we use that dpi for the preview instead of the real one.
				dpi = res.m_ForcedUnityPPI;
			} else {
				dpi = res.m_PPI;
			}

			ForceCanvasScales (dpi);
		}

		static void ForceCanvasScales (float dpi)
		{
			foreach (CanvasScaler scaler in m_CanvasScalers) {

				if (scaler.uiScaleMode != CanvasScaler.ScaleMode.ConstantPhysicalSize)
					continue;

				float num2 = 1f;
				switch (scaler.physicalUnit) {
				case CanvasScaler.Unit.Centimeters:
					num2 = 2.54f;
					break;
				case CanvasScaler.Unit.Millimeters:
					num2 = 25.4f;
					break;
				case CanvasScaler.Unit.Inches:
					num2 = 1f;
					break;
				case CanvasScaler.Unit.Points:
					num2 = 72f;
					break;
				case CanvasScaler.Unit.Picas:
					num2 = 6f;
					break;
				}
				scaler.GetComponent<Canvas> ().scaleFactor = (dpi / num2);
				scaler.GetComponent<Canvas> ().referencePixelsPerUnit = (scaler.referencePixelsPerUnit * num2 / scaler.defaultSpriteDPI);
			}
		}
	}
}