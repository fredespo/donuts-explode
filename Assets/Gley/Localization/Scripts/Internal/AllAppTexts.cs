using System.Collections.Generic;

namespace Gley.Localization.Internal
{
    /// <summary>
    /// Stores all app translations
    /// </summary>
    [System.Serializable]
    public class AllAppTexts
    {
        public List<TranslatedWord> allText = new List<TranslatedWord>();
    }
}
