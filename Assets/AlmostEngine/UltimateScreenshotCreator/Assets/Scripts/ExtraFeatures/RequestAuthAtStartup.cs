using UnityEngine;
using System.Collections;

namespace AlmostEngine.Screenshot.Extra
{
		/// <summary>
		/// Add this script to a scene object to have a iOS gallery permission request popup at startup.
		/// </summary>
		public class RequestAuthAtStartup : MonoBehaviour
		{
				void Start ()
				{
						#if !UNITY_EDITOR && UNITY_IOS
						if(!iOsUtils.HasGalleryAuthorization()){
							iOsUtils.RequestGalleryAuthorization();
						}
						#endif
				}
		}
}