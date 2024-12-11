using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;

public enum Languages
{
    Undefined,
    Unknown,

    Russian,
    Ukrainian,
    Belarusian,

    English,
    Italian,
    Spanish,
    French,
    German,
    Polish,
    Czech,

    Chinese,
    ChineseSimplified,
    Japanese,
    Korean,

    Afrikaans,
    Arabic,
    Basque,
    Bulgarian,
    Catalan,
    Danish,
    Dutch,
    Estonian,
    Faroese,
    Finnish,
    Greek,
    Hebrew,
    Icelandic,
    Indonesian,
    Latvian,
    Lithuanian,
    Norwegian,
    Portuguese,
    Romanian,
    Slovak,
    Slovenian,
    Swedish,
    Thai,
    Turkish,
    Vietnamese,
    Hungarian,
    Urdu
}

public static class Localisation
{

    public static Languages CurrentLanguage = Languages.Undefined;
    static public Dictionary<string, string> Strings;
    static private XmlDocument LoadedLanguage;
    static bool LanguageLoaded = false;
    static TextAsset newbyLanguage;

    public static void DetectLanguage()
    {

        CurrentLanguage = (Languages)Enum.Parse(typeof(Languages), Application.systemLanguage.ToString());

#if UNITY_EDITOR
        if (PlayerPrefs.HasKey("TestLanguage"))
        {
            CurrentLanguage = (Languages)Enum.Parse(typeof(Languages), PlayerPrefs.GetString("TestLanguage"));
        }
        else
        {
            CurrentLanguage = (Languages)Enum.Parse(typeof(Languages), Application.systemLanguage.ToString());
        }
#endif

        //if (PlayerPrefs.GetInt("Language") != 0)
        //{
        //    switch (PlayerPrefs.GetInt("Language"))
        //    {
        //        case 1:
        //            CurrentLanguage = Languages.Urdu;
        //            break;
        //        case 2:
        //            CurrentLanguage = Languages.Arabic;
        //            break;
        //        case 3:
        //            CurrentLanguage = Languages.ChineseSimplified;
        //            break;
        //        case 4:
        //            CurrentLanguage = Languages.English;
        //            break;
        //        case 5:
        //            CurrentLanguage = Languages.French;
        //            break;
        //        case 6:
        //            CurrentLanguage = Languages.German;
        //            break;
        //        case 7:
        //            CurrentLanguage = Languages.Italian;
        //            break;
        //        case 8:
        //            CurrentLanguage = Languages.Polish;
        //            break;
        //        case 9:
        //            CurrentLanguage = Languages.Portuguese;
        //            break;
        //        case 10:
        //            CurrentLanguage = Languages.Spanish;
        //            break;
        //        case 11:
        //            CurrentLanguage = Languages.Turkish;
        //            break;
        //        case 12:
        //            CurrentLanguage = Languages.Ukrainian;
        //            break;
        //        case 13:
        //            CurrentLanguage = Languages.Russian;
        //            break;

        //    }
        //}
        //Debug.Log("DetectedLanguage");

    }

    static public void LoadLanguage()
    {
        Localisation.DetectLanguage();
        LoadedLanguage = new XmlDocument();
        Strings = new Dictionary<string, string>();
        Debug.Log("LoadLanguage");
        newbyLanguage = (TextAsset)Resources.Load("Localisation/" + CurrentLanguage.ToString() + ".xml", typeof(TextAsset));
        if (newbyLanguage == null)
        {
            newbyLanguage = (TextAsset)Resources.Load("Localisation/English.xml", typeof(TextAsset));
        }
        LoadedLanguage.LoadXml(newbyLanguage.text);
        foreach (XmlNode document in LoadedLanguage.ChildNodes)
        {
            foreach (XmlNode newbyString in document.ChildNodes)
            {
                Strings.Add(newbyString.Attributes["name"].Value, newbyString.InnerText);
            }
        }
        LanguageLoaded = true;
    }

    static public Languages GetCurrentLanguage()
    {
        if (LanguageLoaded == false)
        {
            LoadLanguage();
        }
        return CurrentLanguage;
    }

    static public string GetString(string SearchString)
    {
        if (CurrentLanguage == Languages.English) return SearchString;

        if (LanguageLoaded == false)
        {
            LoadLanguage();
        }
        if (Strings.ContainsKey(SearchString))
        {
            return Strings[SearchString];
        }
        else
        {
            return CheckForSimilar(SearchString); ////Unknown String
        }

    }

    static public string CheckForSimilar(string SearchString)
    {
        int nothing;
        int numberPlace = -1;
        string neededNumber = "NAN";
        string result = "";
        string[] splitedString = SearchString.Split(' ');

        for (int i = 0; i < splitedString.Length; i++)
        {
            if (Int32.TryParse(splitedString[i].Replace(",", ""), out nothing))
            {
                neededNumber = splitedString[i];
                numberPlace = i;
                splitedString[i] = "1";
                result = "";
                for (int ii = 0; ii < splitedString.Length; ii++)
                {
                    result += splitedString[ii];
                    if (ii + 1 < splitedString.Length) result += " ";
                }

                if (Strings.ContainsKey(result))
                {

                    string temp = Strings[result];
                    temp = temp.Replace("1", neededNumber.ToString());
                    return temp;
                }

            }
        }

        return SearchString;
    }
} 