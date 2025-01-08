//to serialize a class you must put [System.Serializable] before class declaration
// if you want to serialize just a part from the class, then you put [System.Serializable] before each public property
//you can add here as many properties as you like

namespace Gley.AllPlatformsSave.Internal
{
	[System.Serializable]
	public class GameValues
	{
		public double version = 0;
		public bool showVideo = true;
		public int totalCoins = 0;
		public float musicVolume = 1;
		public string randomText = "Random Text";
	}
}
