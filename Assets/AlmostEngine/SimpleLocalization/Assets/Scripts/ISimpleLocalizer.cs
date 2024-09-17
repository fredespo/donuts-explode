using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlmostEngine.SimpleLocalization
{
	public abstract class ISimpleLocalizer : MonoBehaviour
	{

		public abstract void OnLanguageChanged (string ID);

		public abstract void Save ();

		public abstract void Restore ();

	}
}

