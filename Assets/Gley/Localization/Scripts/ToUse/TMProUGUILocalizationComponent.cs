#if GLEY_TMPRO_LOCALIZATION
using Gley.Localization.Internal;
using TMPro;
using UnityEngine;

namespace Gley.Localization
{
	public class TMProUGUILocalizationComponent : MonoBehaviour,ILocalizationComponent
	{
		public WordIDs wordID;

		/// <summary>
		/// Used for automatically refresh
		/// </summary>
		public void Refresh()
		{
			GetComponent<TextMeshProUGUI>().text = LocalizationManager.Instance.GetText(wordID);
		}

		void Start()
		{
			Refresh();
		}
	}
}
#endif

