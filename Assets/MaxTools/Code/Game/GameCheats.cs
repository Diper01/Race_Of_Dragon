using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MaxTools
{
    using MaxTools.Extensions;

    public class GameCheats : Singleton<GameCheats>
    {
        [SerializeField] Canvas canvas;
        [SerializeField] GameObject menuButtons;
        [SerializeField] GameObject languageButtons;
        [SerializeField] GameObject levelButtons;
        [SerializeField] GameObject levelButtonsContent;
        [SerializeField] GameObject levelButtonPrefab;
        [SerializeField] int levelCount;

        public bool isImmortal
        {
            get;
            private set;
        }

        void Update()
        {
            if (canvas.gameObject.activeSelf)
            {
                return;
            }

            if (Application.isMobilePlatform)
            {
                if (Input.touchCount >= 3)
                {
                    if (this.CheckIntervalNonstop(1.0f, timeMode: TimeMode.UnscaledTime))
                    {
                        ShowMenu();
                    }
                }
            }
            else if (this.CheckInputString("rwdev"))
            {
                ShowMenu();
            }
        }

        void SetEventSystem()
        {
            var selfSystem = GetComponentInChildren<EventSystem>(true);
            var otherSystems = FindObjectsOfType<EventSystem>();

            if (otherSystems.Length == 0 && !selfSystem.gameObject.activeSelf)
            {
                selfSystem.gameObject.SetActive(true);
            }
            else if (otherSystems.Length > 1 && selfSystem.gameObject.activeSelf)
            {
                selfSystem.gameObject.SetActive(false);
            }
        }
        void HideAllButtons()
        {
            menuButtons.SetActive(false);
            languageButtons.SetActive(false);
            levelButtons.SetActive(false);
        }

        public void ShowMenu()
        {
            SetEventSystem();
            HideAllButtons();

            menuButtons.SetActive(true);
            canvas.gameObject.SetActive(true);
        }
        public void HideMenu()
        {
            canvas.gameObject.SetActive(false);
        }

        public void SetImmortal(Text self)
        {
            isImmortal = !isImmortal;

            var on = "On".Colored(Color.green.Mul(0.5f, false));
            var off = "Off".Colored(Color.red);

            self.text = $"Immortal [{(isImmortal ? on : off)}]";

            print($"set immortal {isImmortal}");
        }
        public void SetLanguage(GameObject self)
        {
            HideAllButtons();

            menuButtons.SetActive(true);

            Localization.LoadLanguage(Tools.GetEnumByName<SystemLanguage>(self.name));
        }

        public void LoadLevel(int level)
        {
            print($"load level {level}");
        }
        public void UnlockLevels(Image self)
        {
            self.color = Color.green;

            print($"unlock levels");
        }

        public void AddMoney()
        {
            print($"add money");
        }

        public void ShowLanguageButtons()
        {
            HideAllButtons();

            languageButtons.SetActive(true);
        }
        public void ShowLevelButtons()
        {
            HideAllButtons();

            levelButtons.SetActive(true);

            if (levelButtonsContent.transform.childCount == 0)
            {
                for (int i = 1; i <= levelCount; ++i)
                {
                    int levelNumber = i;
                    Button levelButton = Instantiate(
                        levelButtonPrefab, levelButtonsContent.transform).GetComponent<Button>();
                    levelButton.onClick.AddListener(() => LoadLevel(levelNumber));
                    levelButton.GetComponentInChildren<Text>().text = levelNumber.ToString();
                    levelButton.name = levelNumber.ToString();
                    levelButton.gameObject.SetActive(true);
                }
            }
        }

        void OnGUI()
        {
            return;

            var k = Screen.height / 600.0f;
            var style = new GUIStyle(GUI.skin.button);

            style.fontSize = 14;
            style.fontSize = Tools.CeilingToInt(style.fontSize * k);

            if (GUI.Button(new Rect(0, 0, 70 * k, 30 * k), "Cheats", style))
            {
                ShowMenu();
            }
        }
    }
}
