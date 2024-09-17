using UnityEngine;
using System.Collections;

namespace AlmostEngine.SimpleLocalization
{
	public class LanguageSwitcher : MonoBehaviour
	{
		public void Set(string id) {
			SimpleLocalizationLanguagesAsset.SetLanguage(id);
		}
	}
}
