using UnityEngine;
using UnityEngine.UI;

namespace MaxTools
{
    using MaxTools.Extensions;

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public class TimeDisplay : MonoBehaviour
    {
        Text timeDisplay = null;

        void Start()
        {
            timeDisplay = GetComponent<Text>();
            timeDisplay.text = "Time: 99h 99m 99s";
            name = nameof(TimeDisplay);
        }

        void Update()
        {
            if (Application.isPlaying)
            {
                if (this.CheckChange((int)Time.realtimeSinceStartup, true))
                {
                    timeDisplay.text = $"Time: {Formatters.TimeFormatter(Time.realtimeSinceStartup)}";
                }
            }
        }
    }
}
