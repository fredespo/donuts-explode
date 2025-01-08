using UnityEngine;

namespace Gley.Localization.Internal
{
    /// <summary>
    /// Stores the Settings Window properties
    /// </summary>
    public class LocalizationData : ScriptableObject
    {
        public SupportedLanguages defaultLanguage;
        public int currentLanguage;
        public bool enableTMProSupport;
        public bool enableNGUISupport;
        public bool usePlaymaker;
        public bool useUVS;
    }
}
