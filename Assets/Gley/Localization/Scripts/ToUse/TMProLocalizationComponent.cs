#if GLEY_TMPRO_LOCALIZATION
using Gley.Localization.Internal;
using TMPro;
using UnityEngine;

namespace Gley.Localization
{
	public class TMProLocalizationComponent : MonoBehaviour,ILocalizationComponent
	{
		public WordIDs wordID;

		/// <summary>
		/// Used for automatically refresh
		/// </summary>
		public void Refresh()
		{
			GetComponent<TextMeshPro>().text = LocalizationManager.Instance.GetText(wordID);
		}

		void Start()
		{
			Refresh();
		}
	}
}
#endif

