﻿using UnityEngine;
using SBS.Core;
using System.Collections.Generic;

[AddComponentMenu("OnTheRun/UI/UILinksBar")]
public class UILinksBar : MonoBehaviour
{
    protected OnTheRunInterfaceSounds interfaceSounds;
    void OnDownloadVersionRelease(UIButton button)
    {
        if (interfaceSounds == null)
            interfaceSounds = GameObject.FindGameObjectWithTag("Sounds").GetComponent<OnTheRunInterfaceSounds>();

        interfaceSounds.SendMessage("PlayGeneralInterfaceSound", OnTheRunInterfaceSounds.InterfaceSoundsType.Click);

        Debug.Log("LINK BUTTON: " + button.name);

        GameObject miniclipAPIManagerGo = GameObject.FindGameObjectWithTag("MiniclipAPI");
        if (miniclipAPIManagerGo == null)
            return;

        MiniclipAPIManager miniclipAPIManager = miniclipAPIManagerGo.GetComponent<MiniclipAPIManager>();

        switch (button.name)
        {
            case "btAmazon":
			Application.ExternalEval("window.open('" + miniclipAPIManager.Url_store_Amazon + "','_blank')");  //Application.ExternalEval("window.open('https://www.amazon.com/gp/product/B01ETL9S26','_blank')");
                //Application.ExternalCall("statTracker", "smartphone-links/click/4032/com.miniclip.hotrodracersamazon/7");
                break;

            case "btGoogle":
			Application.ExternalEval("window.open('" + miniclipAPIManager.Url_store_Google + "','_blank')");  //Application.ExternalEval("window.open('https://play.google.com/store/apps/details?id=com.codostudio.captainstrikezombie','_blank')");
                //Application.ExternalCall("statTracker", "smartphone-links/click/4032/com.miniclip.hotrodracers/7");
                break;

            case "btWindowsPhone":
			Application.ExternalEval("window.open('" + miniclipAPIManager.Url_store_Windows + "','_blank')");  //Application.ExternalEval("window.open('https://play.google.com/store/apps/details?id=com.codostudio.captainstrikezombie','_blank')");
                //Application.ExternalCall("statTracker", "smartphone-links/click/4032/ea709b8e-fdf9-44f6-9819-59c52a0d3992/9");
                break;

            case "btAppStore":
			Application.ExternalEval("window.open('" + miniclipAPIManager.Url_store_IOS + "','_blank')");  //Application.ExternalEval("window.open('https://itunes.apple.com/us/app/csz-global-alliance/id1107474631?mt=8','_blank')");
                //Application.ExternalCall("statTracker", "smartphone-links/click/4032/765447392/1");
                break;
        }
    }
}