using UnityEngine;
using SBS.Core;

[AddComponentMenu("OnTheRun/UI/UIFuelGiftPopup")]
public class UIFuelGiftPopup : MonoBehaviour
{
    public UITextField tfTitle;
    public UITextField tfDescription;
    public GameObject icon;

    OnTheRunInterfaceSounds interfaceSounds;

    void OnEnable()
    {

        interfaceSounds = GameObject.FindGameObjectWithTag("Sounds").GetComponent<OnTheRunInterfaceSounds>();
        tfTitle.text = OnTheRunDataLoader.Instance.GetLocaleString("fuel_gift_title");
        tfDescription.text = OnTheRunDataLoader.Instance.GetLocaleString("fuel_gift_description");
    }

    void StartFireworks( )
    {
        OnTheRunFireworks.Instance.StartFireworksEffect(25, transform.Find("fireworks"));
    }
    public GameObject GaragePage;

    void Signal_OnOkSingleButtonRelease(UIButton button)
    {

        PlayerPersistentData.Instance.SaveFirstTimeFuelFinished();
        OnTheRunFuelManager.Instance.Fuel += OnTheRunDataLoader.Instance.GetFirstFuelGift();
        Manager<UIRoot>.Get().UpdateCurrenciesItem();
        interfaceSounds.SendMessage("PlayGeneralInterfaceSound", OnTheRunInterfaceSounds.InterfaceSoundsType.Click);
        GaragePage.GetComponent<UIGaragePage>().uiPlayButton.gameObject.SetActive(true);
        OnTheRunUITransitionManager.Instance.ClosePopup();
    }

    void OnBackButtonAction()
    {
        GameObject.Find("GaragePage").SetActive(true);
        if (Manager<UIManager>.Get().disableInputs)
            return;

        UIButton okButton = null;
        if (transform.Find("content/ResumeButton") != null)
            okButton = transform.Find("content/ResumeButton").GetComponent<UIButton>();

        if (okButton != null && okButton.State != UIButton.StateType.Disabled)
            okButton.onReleaseEvent.Invoke(okButton);
    }
}