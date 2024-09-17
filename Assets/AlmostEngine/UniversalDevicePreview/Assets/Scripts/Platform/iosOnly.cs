using UnityEngine;
using AlmostEngine.Preview;

namespace AlmostEngine.Example.Preview
{

	[ExecuteInEditMode]
	public class iosOnly : PlatformActivate
	{

		public override bool IsGoodPlatform ()
		{
			return DeviceInfo.IsIOS ();
		}

	}
}
