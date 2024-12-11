using UnityEngine;
using System.Collections;
using System;

public class LocalisedString : MonoBehaviour {

	public AdditionalSettingForLanguage[] AdditionalSettingForLanguages;
	UITextField CurrentTextMesh;
    public bool oneShot;

	public void Start () {
		CurrentTextMesh = transform.GetComponent<UITextField>();
		LocaliseString ();
		ApplyAdditionalStringSetting ();

    }

    void OnEnable()
    {
        LocaliseString();
    }

    void Update()
    {
        if(!oneShot)
        LocaliseString();
    }


	void LocaliseString(){
        // if (CurrentTextMesh.text.Contains("You are now driving with your first car")) Debug.Log("SOMEASOFHASFJASFHASKFASKFF");
        if (Localisation.CurrentLanguage == Languages.English) return;
        if (CurrentTextMesh != null)
        {
            string CurrentLocalisedString = Localisation.GetString(CurrentTextMesh.text);
            CurrentTextMesh.text = CurrentLocalisedString;
        }
       
	}

	void ApplyAdditionalStringSetting(){
		//if(AdditionalSettingForLanguages.Length > 0){
		//	for(int i = 0;i < AdditionalSettingForLanguages.Length; i++){
		//		if(Localisation.CurrentLanguage == AdditionalSettingForLanguages[i].Language){
		//			CurrentTextMesh.fontSize =  AdditionalSettingForLanguages[i].FontSize;
		//			if(AdditionalSettingForLanguages[i].FontFile != null){
		//			CurrentTextMesh.font = AdditionalSettingForLanguages[i].FontFile;
		//			CurrentTextMesh.GetComponent<Renderer>().sharedMaterial = AdditionalSettingForLanguages[i].FontFile.material;

		//			}
		//		}
		//	}
		//}
	}
}

[Serializable]
public class AdditionalSettingForLanguage{
	public Languages Language;
	public int FontSize;
	public Font FontFile;
}
