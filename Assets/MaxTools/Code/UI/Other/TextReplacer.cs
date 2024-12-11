using UnityEngine;
using System;

namespace MaxTools
{
    using MaxTools.Extensions;

    [DisallowMultipleComponent]
    public class TextReplacer : MonoBehaviour
    {
        [Serializable]
        public class Marker
        {
            public string identifier = "";
            public StaticVariable staticVariable = null;
            public StaticMethod formatter = null;

            public void Formatted(ref string raw)
            {
                if (identifier.IsNullOrEmpty())
                {
                    Debug.LogWarning("Invalid identifier!");

                    return;
                }

                if (formatter)
                {
                    if (staticVariable)
                    {
                        raw = raw.Replace(identifier, formatter.Invoke(staticVariable.value).ToString());
                    }
                    else
                        raw = raw.Replace(identifier, formatter.Invoke().ToString());
                }
                else
                if (staticVariable)
                {
                    raw = raw.Replace(identifier, staticVariable.value.ToString());
                }
                else
                    Debug.LogWarning("Invalid marker!");
            }
        }

        public Marker[] markers = new Marker[0];
        TextWrapper textWrapper = null;
        string startString = "";

        void Start()
        {
            textWrapper = new TextWrapper(gameObject);
            startString = textWrapper.text;
        }

        void Update()
        {
            if (this.CheckInterval(0.05f))
            {
                var formatted = startString;

                foreach (var marker in markers)
                {
                    marker.Formatted(ref formatted);
                }

                textWrapper.text = formatted;
            }
        }
    }
}
