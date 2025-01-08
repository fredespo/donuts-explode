#if GLEY_UVS_SUPPORT
using Unity.VisualScripting;

namespace Gley.Localization.Internal
{
    [IncludeInSettings(true)]
    public static class LocalizationUVS
    {
        public static string GetCurrentLanguage()
        {
            return Gley.Localization.API.GetCurrentLanguage().ToString();
        }

        public static void SetCurrentLanguage(SupportedLanguages language)
        {
            Gley.Localization.API.SetCurrentLanguage(language);
        }

        public static void NextLanguage()
        {
            Gley.Localization.API.NextLanguage();
            Gley.Localization.API.SetCurrentLanguage(Gley.Localization.API.GetCurrentLanguage());
        }

        public static void PreviousLanguage()
        {
            Gley.Localization.API.PreviousLanguage();
            Gley.Localization.API.SetCurrentLanguage(Gley.Localization.API.GetCurrentLanguage());
        }

        public static string GetText(string id)
        {
            return Gley.Localization.API.GetText(id);
        }

        public static string GetText(WordIDs id)
        {
            return Gley.Localization.API.GetText(id);
        }
    }
}
#endif
