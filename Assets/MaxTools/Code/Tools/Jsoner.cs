using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace MaxTools
{
    using MaxTools.Extensions;

    public static class Jsoner
    {
        public static object FromJson(string json, Type type)
        {
            return JsonUtility.FromJson(json, type);
        }
        public static T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
        public static void FromJsonOverwrite(string json, object objectToOverwrite)
        {
            JsonUtility.FromJsonOverwrite(json, objectToOverwrite);
        }

        public static string ToJson(object _object)
        {
            return DeleteIgnoredFields(_object, JsonUtility.ToJson(_object));
        }
        public static string ToJson(object _object, bool prettyPrint)
        {
            return DeleteIgnoredFields(_object, JsonUtility.ToJson(_object, prettyPrint));
        }

        public static bool SaveData(object _object, string playerPrefsKey, bool encrypt = false)
        {
            if (string.IsNullOrEmpty(playerPrefsKey) || string.IsNullOrWhiteSpace(playerPrefsKey))
            {
                _object.DebugLogError($"Invalid {nameof(playerPrefsKey)}!");

                return false;
            }

            string json = ToJson(_object);

            if (encrypt)
            {
                json = Tools.StringEncrypt(json);
            }

            PlayerPrefs.SetString(playerPrefsKey, json);
            PlayerPrefs.Save();

            _object.DebugLog("Data has been saved!");

            return true;
        }
        public static bool LoadData(object objectToOverwrite, string playerPrefsKey, bool decrypt = false)
        {
            if (string.IsNullOrEmpty(playerPrefsKey) || string.IsNullOrWhiteSpace(playerPrefsKey))
            {
                objectToOverwrite.DebugLogError($"Invalid {nameof(playerPrefsKey)}!");

                return false;
            }

            if (!PlayerPrefs.HasKey(playerPrefsKey))
            {
                objectToOverwrite.DebugLogWarning("PlayerPrefs key not found!");

                return false;
            }

            string json = PlayerPrefs.GetString(playerPrefsKey);

            if (decrypt)
            {
                json = Tools.StringDecrypt(json);
            }

            FromJsonOverwrite(json, objectToOverwrite);

            objectToOverwrite.DebugLog("Data has been loaded!");

            return true;
        }

        static string DeleteIgnoredFields(object _object, string json)
        {
            var parseState = ParseState.VariableSearchStart;
            var variablePath = "";
            var variableStartIndex = 0;

            for (int i = 0; i < json.Length; ++i)
            {
                switch (parseState)
                {
                    case ParseState.VariableSearchStart:
                        {
                            if (json[i] == '"')
                            {
                                variableStartIndex = i + 1;

                                parseState = ParseState.VariableSearchEnd;
                            }
                        }
                        break;

                    case ParseState.VariableSearchEnd:
                        {
                            if (json[i] == '"')
                            {
                                var variableName = json.Substring(variableStartIndex, i - variableStartIndex);

                                if (variablePath == "")
                                {
                                    variablePath = variableName;
                                }
                                else
                                    variablePath += "." + variableName;

                                var fieldInfo = GetNestedField(_object, variablePath);

                                if (fieldInfo.IsDefined(typeof(JsonIgnoreAttribute), true))
                                {
                                    parseState = ParseState.VariableDeletion;
                                }
                                else
                                    parseState = ParseState.VariableSkip;
                            }
                        }
                        break;

                    case ParseState.VariableDeletion:
                        {

                        }
                        break;

                    case ParseState.VariableSkip:
                        {

                        }
                        break;
                }
            }

            return json;
        }
        static FieldInfo GetNestedField(object _object, string path)
        {
            string[] variableNames = path.Split('.', '/', '\\');

            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            FieldInfo fieldInfo = _object.GetType().GetField(variableNames[0], bindingFlags);

            for (int i = 1; i < variableNames.Length; ++i)
            {
                fieldInfo = fieldInfo.FieldType.GetField(variableNames[i], bindingFlags);
            }

            return fieldInfo;
        }

        enum ParseState
        {
            VariableSearchStart,
            VariableSearchEnd,
            VariableDeletion,
            VariableSkip
        }
    }
}
