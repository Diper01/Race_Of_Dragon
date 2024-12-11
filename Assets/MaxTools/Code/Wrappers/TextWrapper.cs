//#define MAXTOOLS_TMP_MOCK

using UnityEngine;
using UnityEngine.UI;
using System;

#if MAXTOOLS_TMP_MOCK
namespace TMPro
{
    public class TMP_Text : Component
    {
        public string text;
    }
}
#endif

namespace MaxTools
{
    public class TextWrapper
    {
        GameObject target;

        Text textComponent;
        TextMesh textMeshComponent;

#if MAXTOOLS_TMP || MAXTOOLS_TMP_MOCK
        TMPro.TMP_Text TMP_Component;
#endif
        Func<string> getter;
        Action<string> setter;

        public TextWrapper(GameObject target)
        {
            this.target = target;
        }

        public bool isValid
        {
            get
            {
                if (target == null)
                    return false;

                if (textComponent != null)
                    return true;

                if (textMeshComponent != null)
                    return true;

#if MAXTOOLS_TMP || MAXTOOLS_TMP_MOCK
                if (TMP_Component != null)
                    return true;
#endif
                // Text
                textComponent = target.GetComponent<Text>();

                if (textComponent != null)
                {
                    getter = () => textComponent.text;
                    setter = (value) => textComponent.text = value;
                    return true;
                }

                // TextMesh
                textMeshComponent = target.GetComponent<TextMesh>();

                if (textMeshComponent != null)
                {
                    getter = () => textMeshComponent.text;
                    setter = (value) => textMeshComponent.text = value;
                    return true;
                }

#if MAXTOOLS_TMP || MAXTOOLS_TMP_MOCK
                // TMP
                TMP_Component = target.GetComponent<TMPro.TMP_Text>();

                if (TMP_Component != null)
                {
                    getter = () => TMP_Component.text;
                    setter = (value) => TMP_Component.text = value;
                    return true;
                }
#endif
                return false;
            }
        }

        public string text
        {
            get
            {
                if (isValid)
                {
                    return getter();
                }
                else
                    throw new Exception("Text component not found!");
            }

            set
            {
                if (isValid)
                {
                    setter(value);
                }
                else
                    throw new Exception("Text component not found!");
            }
        }
    }
}
