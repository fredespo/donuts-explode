using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlmostEngine.Examples
{
	public class MenuController : MonoBehaviour
	{
		public CameraController m_Player;

		void Awake ()
		{
			if (m_Player == null)
				return;
			m_Player.enabled = false;
		}

		public void OnButtonClickCallback ()
		{
			if (m_Player == null)
				return;
			m_Player.enabled = true;
			gameObject.SetActive (false);
		}
	}
}