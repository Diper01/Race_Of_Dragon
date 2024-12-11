using UnityEngine;
using System.Collections;
using SBS.Core;

public class OnTheRunBackButtonManager : MonoBehaviour
{
    #region Singleton instance
    protected static OnTheRunBackButtonManager instance = null;

	private int LastNotificationId = 0;


    public static OnTheRunBackButtonManager Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    bool shouldUseBackButton;
    UIHomeButton uiHomeButton;
    UIManager uiManager;

    void Awake()
    {

        //.Assert(null == instance);
        instance = this;

        GameObject.DontDestroyOnLoad(gameObject);

        Init();
    }

    void OnDestroy()
    {
        //.Assert(this == instance);
        instance = null;
    }

    void Init()
    {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_WP8
        shouldUseBackButton = true;
#else
        shouldUseBackButton = false;
#endif

        uiManager = Manager<UIManager>.Get();
        //.Assert(uiManager != null);

        if (!shouldUseBackButton)
            Destroy(this);
    }

    public void SetHomeButton(UIHomeButton homeButton)
    {
        uiHomeButton = homeButton;
    }

#if UNITY_ANDROID || UNITY_WP8
    bool androidHasFocusBackup = true;
#endif

    public GameObject areUSure;
    void Start()
    {
        if (FindObjectOfType<QuitScript>() != null)
        areUSure = FindObjectOfType<QuitScript>().areUSure;
    }
    void Update()
    {
        if (GameObject.Find("StartPage") != null && GameObject.Find("StartPage").activeInHierarchy)
            uiManager.disableInputs = false;
        if (Input.GetKeyUp(KeyCode.Escape) && GameObject.Find("StartPage") != null && GameObject.Find("StartPage").activeInHierarchy)
        {

            if (!areUSure.activeInHierarchy) areUSure.SetActive(true);
            else areUSure.SetActive(false);
        }


        //UIManager.Instance.disableInputs = !AndroidUtils.HasFocus();

#if UNITY_ANDROID || UNITY_WP8
        bool hasFocus = AndroidUtils.HasFocus();

        if (androidHasFocusBackup != hasFocus)
        {
            androidHasFocusBackup = hasFocus;
            UIManager.Instance.disableInputs = !androidHasFocusBackup;
            ManageAndroidHasFocusPause();
        }

        if (!androidHasFocusBackup)
            UIManager.Instance.disableInputs = true;
#endif

        if (ShouldManageBackKeyEvents() && !uiManager.disableInputs)
        {
            UIPopup frontPopup = uiManager.FrontPopup;
            UIPage activePage = uiManager.ActivePage;
            string activePageName = uiManager.ActivePageName;

            if (frontPopup != null && !(frontPopup.name.Equals("OptionsPopup") ||
                                        frontPopup.name.Equals("CurrencyPopup") ||
                                        frontPopup.name.Equals("LoadingPopup") ||
                                        frontPopup.name.Equals("WheelPopup") ||
                                        frontPopup.name.Equals("FBFriendsPopup") ||
                                        frontPopup.name.Equals("TutorialPopup")))
            {
                //Debug.Log("BackButton - Message to FrontPopup - popup: " + frontPopup.name);
                frontPopup.gameObject.SendMessage("OnBackButtonAction", SendMessageOptions.DontRequireReceiver);
                return;
            }

            if (activePage != null && activePageName.Equals("RankingsPage"))
            {
                //Debug.Log("BackButton - Message to ActivePage - page: " + activePage);
                activePage.gameObject.SendMessage("OnBackButtonAction", SendMessageOptions.DontRequireReceiver);
            }

            if (uiHomeButton != null && uiHomeButton.gameObject.activeInHierarchy)
            {
                if (uiHomeButton.GetComponent<UIButton>().State != UIButton.StateType.Disabled)
                {
                    //Debug.Log("BackButton - Home Button Action");
                    uiHomeButton.gameObject.SendMessage("OnBackButtonAction", SendMessageOptions.DontRequireReceiver);
                    return;
                }
            }

            if (activePage != null && !activePageName.Equals("StartPage"))
            {
                //Debug.Log("BackButton - Message to ActivePage - page: " + activePage);
                activePage.gameObject.SendMessage("OnBackButtonAction", SendMessageOptions.DontRequireReceiver);
                return;
            }

            if (activePage != null && activePageName.Equals("StartPage"))
                QuitGame();
        }
    }

    void ManageAndroidHasFocusPause()
    {
#if UNITY_ANDROID
        UIManager uiManager = Manager<UIManager>.Get();
        if (uiManager.ActivePageName.Equals("IngamePage"))
        {
            if (!androidHasFocusBackup)
            {/*
                UIPopup frontPopup = uiManager.FrontPopup;
                if (null == frontPopup || !frontPopup.pausesGame) // || frontPopup.name != "RewardPopup")
                {*/
                    if (!uiManager.BringPopupToFront("PausePopup"))
                    {
                        OnTheRunGameplay onTheRunGameplay = GameObject.FindGameObjectWithTag("GameplayManagers").GetComponent<OnTheRunGameplay>();
                        if (onTheRunGameplay != null)
                            onTheRunGameplay.ChangeState(OnTheRunGameplay.GameplayStates.Paused);
                        uiManager.PushPopup("PausePopup");
                    }
                //}
            }
        }
#endif
    }

    bool ShouldManageBackKeyEvents()
    {
        bool manageBackKeyEvents = false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            manageBackKeyEvents = true;
#if UNITY_ANDROID && !UNITY_EDITOR
          
#endif
        }

        return manageBackKeyEvents;
    }

    void QuitGame()
    {
        if (areUSure.activeInHierarchy == true) return;

        PlayerPersistentData.Instance.Save();
#if UNITY_EDITOR
        Debug.Log("Should Quit the Game now..."+ " "+this.gameObject.name);
        areUSure.SetActive(true);
#elif UNITY_ANDROID
        areUSure.SetActive(true);
#elif UNITY_WP8
        SBS.Miniclip.WP8Bindings.OnShowExitPopup(string.Empty, OnTheRunDataLoader.Instance.GetLocaleString("quit_confirm"));
#endif
    }

    public void AndroidQuitGame()
    {
        // Google apps removed
        //LastNotificationId = AndroidNotificationManager.instance.ScheduleLocalNotification("Hello", "A new dragon is waiting to be explored !", 1);
        Application.Quit ();
    }

    void onExitPopupClicked(string btnIdx)
    {
#if UNITY_ANDROID
        NativeDispatcher.Instance.RemoveEvent("alertButtonClicked");
        if (btnIdx == OnTheRunDataLoader.Instance.GetLocaleString("yes"))
        {
            Application.Quit();
        }
#endif
    }
}