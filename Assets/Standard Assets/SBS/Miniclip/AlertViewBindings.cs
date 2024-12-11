using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SBS.Miniclip
{
    public class AlertViewBindings
    {
#if UNITY_IPHONE && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void openAlertBox(
            string title,
            string message,
            string button,
            string[] otherButtons,
            int otherButtonsCount,
            bool pauseUnity);
#endif       
	}
}