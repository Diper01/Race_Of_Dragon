using System;
using System.Collections;
using UnityEngine;

public static class AndroidUtils
{

    public static string GetVersionName()
    {
#if UNITY_EDITOR || !UNITY_ANDROID
        return "1.0";
#else
		return "1.0";
#endif
    }

    public static bool HasFocus()
    {
#if UNITY_EDITOR || !UNITY_ANDROID
        return true;
#else
		return true;

#endif
    }

    public static void Quit()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		Application.Quit();
#endif
    }
}
