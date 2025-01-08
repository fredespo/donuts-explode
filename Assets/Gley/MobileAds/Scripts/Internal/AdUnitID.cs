namespace Gley.MobileAds.Internal
{
    [System.Serializable]
    public class AdUnitID
    {
        public string id;
        public string displayName;
        public bool required;
        public AdUnitID(string displayName)
        {
            SetDisplayName(displayName);
        }

        public void SetDisplayName(string displayName)
        {
            this.displayName = displayName;
            if (string.IsNullOrEmpty(displayName))
            {
                required = false;
                return;
            }
            required = true;
        }
    }
}