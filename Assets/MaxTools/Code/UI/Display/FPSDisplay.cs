using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MaxTools
{
    using MaxTools.Extensions;

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public class FPSDisplay : MonoBehaviour
    {
        Text fpsDisplay = null;
        Queue<float> fpsQueue = null;

        void Start()
        {
            fpsDisplay = GetComponent<Text>();
            fpsDisplay.text = "FPS: 1234";
            fpsQueue = new Queue<float>();
            name = nameof(FPSDisplay);
        }

        void Update()
        {
            if (Application.isPlaying)
            {
                fpsQueue.Enqueue(1.0f / Time.unscaledDeltaTime);

                if (fpsQueue.Count > 100)
                {
                    fpsQueue.Dequeue();

                    if (this.CheckInterval(0.05f, timeMode: TimeMode.UnscaledTime))
                    {
                        float average = Tools.Average(fpsQueue.ToArray());

                        fpsDisplay.text = $"FPS: {average:F0}";
                    }
                }
            }
        }
    }
}
