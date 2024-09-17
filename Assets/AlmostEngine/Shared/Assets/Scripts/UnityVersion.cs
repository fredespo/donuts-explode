using UnityEngine;
using System.Collections;

namespace AlmostEngine
{
		public class UnityVersion
		{
				public static bool HasPro ()
				{
			
						#if (UNITY_5 || UNITY_5_6_OR_NEWER)
						return true;
						#else
						return Application.HasProLicense ();
						#endif
				}
	
		}
}
