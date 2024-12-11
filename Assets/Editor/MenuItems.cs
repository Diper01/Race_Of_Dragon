using UnityEngine;
using UnityEditor;

public class MenuItems
{

//Переключение языков из меню

	[MenuItem("Localisation/Undefined",false,1)]
	private static void SetLanguageUndefined()
	{
		PlayerPrefs.SetString("TestLanguage","Undefined");
	}
	[MenuItem("Localisation/Unknown",false,1)]
	private static void SetLanguageUnknown()
	{
		PlayerPrefs.SetString("TestLanguage","Unknown");
	}
	[MenuItem("Localisation/Russian",false,12)]
	private static void SetLanguageRU()
	{
		PlayerPrefs.SetString("TestLanguage","Russian");
	}
	[MenuItem("Localisation/Ukrainian",false,12)]
	private static void SetLanguageUA()
	{
		PlayerPrefs.SetString("TestLanguage","Ukrainian");
	}
	[MenuItem("Localisation/Belarusian",false,12)]
	private static void SetLanguageBR()
	{
		PlayerPrefs.SetString("TestLanguage","Belarusian");
	}
	[MenuItem("Localisation/English",false,23)]
	private static void SetLanguageEN()
	{
		PlayerPrefs.SetString("TestLanguage","English");
	}
	[MenuItem("Localisation/Italian",false,23)]
	private static void SetLanguageIT()
	{
		PlayerPrefs.SetString("TestLanguage","Italian");
	}
	[MenuItem("Localisation/Spanish",false,23)]
	private static void SetLanguageSP()
	{
		PlayerPrefs.SetString("TestLanguage","Spanish");
	}
	[MenuItem("Localisation/French",false,23)]
	private static void SetLanguageFR()
	{
		PlayerPrefs.SetString("TestLanguage","French");
	}
	[MenuItem("Localisation/German",false,23)]
	private static void SetLanguageDE()
	{
		PlayerPrefs.SetString("TestLanguage","German");
	}
	[MenuItem("Localisation/Polish",false,23)]
	private static void SetLanguagePL()
	{
		PlayerPrefs.SetString("TestLanguage","Polish");
	}
	[MenuItem("Localisation/Czech",false,23)]
	private static void SetLanguageCZ()
	{
		PlayerPrefs.SetString("TestLanguage","Czech");
	}
	[MenuItem("Localisation/Chinese",false,34)]
	private static void SetLanguageCN()
	{
		PlayerPrefs.SetString("TestLanguage","Chinese");
	}
	[MenuItem("Localisation/Japanese",false,34)]
	private static void SetLanguageJP()
	{
		PlayerPrefs.SetString("TestLanguage","Japanese");
	}
	[MenuItem("Localisation/Korean",false,34)]
	private static void SetLanguageKR()
	{
		PlayerPrefs.SetString("TestLanguage","Korean");
	}
	[MenuItem("Localisation/Afrikaans")]
	private static void SetLanguageAF()
	{
		PlayerPrefs.SetString("TestLanguage","Afrikaans");
	}
	[MenuItem("Localisation/Arabic")]
	private static void SetLanguageAR()
	{
		PlayerPrefs.SetString("TestLanguage","Arabic");
	}
	[MenuItem("Localisation/Basque")]
	private static void SetLanguageBS()
	{
		PlayerPrefs.SetString("TestLanguage","Basque");
	}
	[MenuItem("Localisation/Bulgarian")]
	private static void SetLanguageBG()
	{
		PlayerPrefs.SetString("TestLanguage","Bulgarian");
	}
	[MenuItem("Localisation/Catalan")]
	private static void SetLanguageCT()
	{
		PlayerPrefs.SetString("TestLanguage","Catalan");
	}
	[MenuItem("Localisation/Danish")]
	private static void SetLanguageDA()
	{
		PlayerPrefs.SetString("TestLanguage","Danish");
	}
	[MenuItem("Localisation/Dutch")]
	private static void SetLanguageDC()
	{
		PlayerPrefs.SetString("TestLanguage","Dutch");
	}
	[MenuItem("Localisation/Estonian")]
	private static void SetLanguageET()
	{
		PlayerPrefs.SetString("TestLanguage","Estonian");
	}
	[MenuItem("Localisation/Faroese")]
	private static void SetLanguageFA()
	{
		PlayerPrefs.SetString("TestLanguage","Faroese");
	}
	[MenuItem("Localisation/Finnish")]
	private static void SetLanguageFN()
	{
		PlayerPrefs.SetString("TestLanguage","Finnish");
	}
	[MenuItem("Localisation/Greek")]
	private static void SetLanguageGR()
	{
		PlayerPrefs.SetString("TestLanguage","Greek");
	}
	[MenuItem("Localisation/Hebrew")]
	private static void SetLanguageHR()
	{
		PlayerPrefs.SetString("TestLanguage","Hebrew");
	}
	[MenuItem("Localisation/Icelandic")]
	private static void SetLanguageIC()
	{
		PlayerPrefs.SetString("TestLanguage","Icelandic");
	}
	[MenuItem("Localisation/Indonesian")]
	private static void SetLanguageIN()
	{
		PlayerPrefs.SetString("TestLanguage","Indonesian");
	}
	[MenuItem("Localisation/Latvian")]
	private static void SetLanguageLT()
	{
		PlayerPrefs.SetString("TestLanguage","Latvian");
	}
	[MenuItem("Localisation/Lithuanian")]
	private static void SetLanguageLI()
	{
		PlayerPrefs.SetString("TestLanguage","Lithuanian");
	}
	[MenuItem("Localisation/Norwegian")]
	private static void SetLanguageNO()
	{
		PlayerPrefs.SetString("TestLanguage","Norwegian");
	}
	[MenuItem("Localisation/Portuguese")]
	private static void SetLanguagePR()
	{
		PlayerPrefs.SetString("TestLanguage","Portuguese");
	}
	[MenuItem("Localisation/Romanian")]
	private static void SetLanguageRO()
	{
		PlayerPrefs.SetString("TestLanguage","Romanian");
	}
	[MenuItem("Localisation/Slovak")]
	private static void SetLanguageSL()
	{
		PlayerPrefs.SetString("TestLanguage","Slovak");
	}
	[MenuItem("Localisation/Slovenian")]
	private static void SetLanguageSN()
	{
		PlayerPrefs.SetString("TestLanguage","Slovenian");
	}
	[MenuItem("Localisation/Swedish")]
	private static void SetLanguageSW()
	{
		PlayerPrefs.SetString("TestLanguage","Swedish");
	}
	[MenuItem("Localisation/Thai")]
	private static void SetLanguageTI()
	{
		PlayerPrefs.SetString("TestLanguage","Thai");
	}
	[MenuItem("Localisation/Turkish")]
	private static void SetLanguageTU()
	{
		PlayerPrefs.SetString("TestLanguage","Turkish");
	}
	[MenuItem("Localisation/Vietnamese")]
	private static void SetLanguageVN()
	{
		PlayerPrefs.SetString("TestLanguage","Vietnamese");
	}
	[MenuItem("Localisation/Hungarian")]
	private static void SetLanguageHU()
	{
		PlayerPrefs.SetString("TestLanguage","Hungarian");
	}

//Добавление объектов на сцену при помощи меню

	//Добавление локализированного 3D текста
    [MenuItem("GameObject/3D Object/Localised 3D Text")]
	private static void CreateLocalised3DText()
	{
		Object Localised3DTextPrefab = AssetDatabase.LoadAssetAtPath("Assets/Localisation/Prefabs/3DText.prefab",typeof(GameObject));
		GameObject Localised3DText = PrefabUtility.InstantiateAttachedAsset(Localised3DTextPrefab) as GameObject;
		Localised3DText.name = "Localised3DText";
		Selection.activeGameObject = Localised3DText;
	}

	//Добавление компонента локализации к UI
	[MenuItem ("Component/UI/Localisation", priority=30)]
	private static void  AssignUITextLocalisation () {
		if (Selection.activeTransform != null) {
			if(Selection.activeTransform.GetComponent<UnityEngine.UI.Text>() != null){
				if(Selection.activeTransform.GetComponent<LocalisedUIText>() == null){
					GameObject selectedGameObject = Selection.activeGameObject;
					selectedGameObject.AddComponent<LocalisedUIText> ();
				}else{
					EditorUtility.DisplayDialog ("Can't add script","Can't add 'Localisation' because a 'Localisation' is already added to the game object! A GameObject can only contain one 'Localisation' component","Ok");
				}
			}else{
				EditorUtility.DisplayDialog ("Can't add script","Can't add 'Localisation' because a 'Text' component is missed","Ok");
			}
		}


	}
	[MenuItem ("Component/UI/Localisation", true, priority=30)]
	private static bool  ValidateUITextLocalisation () {
		if (Selection.activeTransform != null) {
			return Selection.activeTransform.GetComponent<UnityEngine.UI.Text>() != null;
		}
		return false;
	}

	//Удаление PlayerPrefs
	[MenuItem("Tools/Clear PlayerPrefs")]
	private static void NewMenuOption()
	{
		PlayerPrefs.DeleteAll();
	}
}

