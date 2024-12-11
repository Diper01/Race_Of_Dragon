using UnityEngine;

namespace MaxTools
{
    using MaxTools.Extensions;

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class LocalizedString : MonoBehaviour
    {
        [SerializeField] bool localizeInEditor = true;
        [SerializeField] bool useSpecialCharacters = true;
        [SerializeField]
        [TextArea(3, 9)] string rawString = "";

        TextWrapper textWrapper;

        void Start()
        {
            if (Application.isPlaying)
            {
                textWrapper = new TextWrapper(gameObject);

                textWrapper.text = Localization.GetLocalizedString(rawString, useSpecialCharacters, gameObject);
            }
        }

        void Update()
        {
            if (Application.isPlaying)
            {
                if (this.CheckChange(rawString))
                {
                    textWrapper.text = Localization.GetLocalizedString(rawString, useSpecialCharacters, gameObject);
                }

                if (this.CheckChange(Localization.currentLanguage))
                {
                    textWrapper.text = Localization.GetLocalizedString(rawString, useSpecialCharacters, gameObject);
                }
            }
            else
            {
                if (textWrapper == null)
                {
                    textWrapper = new TextWrapper(gameObject);
                }

                if (localizeInEditor)
                {
                    textWrapper.text = Localization.GetLocalizedString(rawString, useSpecialCharacters, gameObject);
                }
                else
                    textWrapper.text = rawString;
            }
        }
    }
}
