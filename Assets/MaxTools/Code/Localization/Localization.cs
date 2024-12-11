using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Xml;
using System.Linq;
using System.Collections.Generic;

namespace MaxTools
{
    using MaxTools.Extensions;

    public static class Localization
    {
        static Dictionary<string, string> aliasDictionary = null;
        static List<string> aliasKeys = null;

        public static int maxNumberOfIterations => 100;
        public static string debugLanguageKey => "[Localization] Debug Language";

        public static SystemLanguage defaultLanguage => SystemLanguage.English;
        public static SystemLanguage currentLanguage { get; private set; } = SystemLanguage.Unknown;

        public static bool isLanguageLoaded => currentLanguage != SystemLanguage.Unknown;

        public static void LoadLanguage(SystemLanguage language)
        {
            var textAssets = Resources.LoadAll<TextAsset>("Localization");

            if (textAssets.Length == 0)
            {
                Debug.LogError("Localization files not found!");

                return;
            }

            var requiredTextAssets = new List<TextAsset>();

            foreach (var textAsset in textAssets)
            {
                if (textAsset.name == language.ToString())
                {
                    requiredTextAssets.Add(textAsset);
                }
            }

            if (requiredTextAssets.Count == 0)
            {
                if (language != defaultLanguage)
                {
                    Debug.LogError($"Localization file '{language}.xml' not found!\nDefault language will be loaded -> {defaultLanguage}");

                    LoadLanguage(defaultLanguage);

                    return;
                }
                else
                {
                    Debug.LogError($"Default localization file '{language}.xml' not found!");

                    return;
                }
            }

            aliasDictionary = new Dictionary<string, string>();

            foreach (var textAsset in requiredTextAssets)
            {
                var xmlFile = new XmlDocument();

                xmlFile.LoadXml(textAsset.text);

                foreach (XmlNode node in xmlFile.DocumentElement.SelectNodes("//string"))
                {
                    string key = node.Attributes["name"].Value;

                    if (aliasDictionary.ContainsKey(key))
                    {
                        var message = $"Alias '{key}' already exists in file '{language}.xml'.";
#if UNITY_EDITOR
                        message += "\n" + AssetDatabase.GetAssetPath(textAsset);
#endif
                        Debug.LogError(message, textAsset);
                    }
                    else
                        aliasDictionary.Add(key, node.InnerText);
                }
            }

            aliasKeys = aliasDictionary.Keys.OrderByDescending((k) => k.Length).ToList();

            currentLanguage = language;

            Debug.Log($"{currentLanguage} has been loaded successfully!");
        }
        public static void LoadLanguageSystem()
        {
            var language = Application.systemLanguage;

#if UNITY_EDITOR
            if (EditorPrefs.HasKey(debugLanguageKey))
            {
                language = Tools.GetEnumByName<SystemLanguage>(EditorPrefs.GetString(debugLanguageKey));
            }
#endif
            LoadLanguage(language);
        }

        public static string GetLocalizedString(string rawString, bool useSpecialCharacters = true, UnityEngine.Object context = null)
        {
            if (rawString.IsNullOrWhiteSpace())
            {
                Debug.LogWarning("Empty string!", context);

                return rawString;
            }

            if (!isLanguageLoaded)
            {
                LoadLanguageSystem();
            }

            string localizedString = rawString;

            int i = 0;

            for (; i < maxNumberOfIterations; ++i)
            {
                string oldLocalizedString = localizedString;

                foreach (string aliasKey in aliasKeys)
                {
                    string aliasValue = aliasDictionary[aliasKey];

                    if (useSpecialCharacters)
                    {
                        aliasValue = aliasValue.Replace("\\a", "\a");
                        aliasValue = aliasValue.Replace("\\b", "\b");
                        aliasValue = aliasValue.Replace("\\f", "\f");
                        aliasValue = aliasValue.Replace("\\n", "\n");
                        aliasValue = aliasValue.Replace("\\r", "\r");
                        aliasValue = aliasValue.Replace("\\t", "\t");
                        aliasValue = aliasValue.Replace("\\v", "\v");
                        aliasValue = aliasValue.Replace("\\0", "\0");
                    }

                    localizedString = localizedString.Replace(aliasKey, aliasValue);
                }

                if (localizedString == oldLocalizedString)
                {
                    break;
                }
            }

            if (localizedString == rawString)
            {
                Debug.LogWarning($"Without changes!\nrawString -> '{rawString}'", context);
            }
            else if (i == maxNumberOfIterations)
            {
                Debug.LogWarning($"Maximum number of iterations ({maxNumberOfIterations}) has been reached.", context);
            }

            if (currentLanguage == SystemLanguage.Arabic)
            {
                localizedString = localizedString.Reverse();
            }

            return localizedString;
        }
    }
}
