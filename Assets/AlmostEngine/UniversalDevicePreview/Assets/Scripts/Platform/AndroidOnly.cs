using UnityEngine;

using AlmostEngine.Preview;

namespace AlmostEngine.Example.Preview
{

	[ExecuteInEditMode]
	public class AndroidOnly : PlatformActivate
	{

		public override bool IsGoodPlatform ()
		{
			return DeviceInfo.IsAndroid ();
		}

	}
}
