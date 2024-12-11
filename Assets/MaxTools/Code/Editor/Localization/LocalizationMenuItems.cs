using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    public static class LocalizationMenuItems
    {
        [MenuItem("MaxTools/Localization/Clear Debug Language", priority = 15)]
        static void ClearDebugLanguage()
        {
            EditorPrefs.DeleteKey("Debug Language");
            Debug.Log($"Debug Language -> DELETED");
        }

        [MenuItem("MaxTools/Localization/Unknown", priority = 30)]
        static void SetLanguageUnknown()
        {
            ApplyLanguage(SystemLanguage.Unknown);
        }

        [MenuItem("MaxTools/Localization/Russian", priority = 45)]
        static void SetLanguageRussian()
        {
            ApplyLanguage(SystemLanguage.Russian);
        }

        [MenuItem("MaxTools/Localization/Ukrainian", priority = 45)]
        static void SetLanguageUkrainian()
        {
            ApplyLanguage(SystemLanguage.Ukrainian);
        }

        [MenuItem("MaxTools/Localization/English", priority = 60)]
        static void SetLanguageEnglish()
        {
            ApplyLanguage(SystemLanguage.English);
        }

        [MenuItem("MaxTools/Localization/Italian", priority = 60)]
        static void SetLanguageItalian()
        {
            ApplyLanguage(SystemLanguage.Italian);
        }

        [MenuItem("MaxTools/Localization/Spanish", priority = 60)]
        static void SetLanguageSpanish()
        {
            ApplyLanguage(SystemLanguage.Spanish);
        }

        [MenuItem("MaxTools/Localization/French", priority = 60)]
        static void SetLanguageFrench()
        {
            ApplyLanguage(SystemLanguage.French);
        }

        [MenuItem("MaxTools/Localization/German", priority = 60)]
        static void SetLanguageGerman()
        {
            ApplyLanguage(SystemLanguage.German);
        }

        [MenuItem("MaxTools/Localization/Arabic", priority = 60)]
        static void SetLanguageArabic()
        {
            ApplyLanguage(SystemLanguage.Arabic);
        }

        [MenuItem("MaxTools/Localization/Portuguese", priority = 60)]
        static void SetLanguagePortuguese()
        {
            ApplyLanguage(SystemLanguage.Portuguese);
        }

        [MenuItem("MaxTools/Localization/Polish", priority = 60)]
        static void SetLanguagePolish()
        {
            ApplyLanguage(SystemLanguage.Polish);
        }

        [MenuItem("MaxTools/Localization/Turkish", priority = 60)]
        static void SetLanguageTurkish()
        {
            ApplyLanguage(SystemLanguage.Turkish);
        }

        static void ApplyLanguage(SystemLanguage language)
        {
            EditorPrefs.SetString(Localization.debugLanguageKey, $"{language}");
            Debug.Log($"[Localization] Debug Language -> {language}");
            Localization.LoadLanguage(language);
        }
    }
}
