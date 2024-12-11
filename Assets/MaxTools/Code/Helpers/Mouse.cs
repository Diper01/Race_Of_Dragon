using UnityEngine;

namespace MaxTools
{
    using MaxTools.Extensions;

    [DefaultExecutionOrder(int.MinValue)]
    public class Mouse : Singleton<Mouse>
    {
        Vector3 m_DeltaWorldPosition = Vector3.zero;
        Vector3 m_DeltaScreenPosition = Vector3.zero;

        bool m_IsDoubleClick0 = false;
        bool m_IsDoubleClick1 = false;

        void Update()
        {
            m_DeltaWorldPosition = worldPosition - this.GetPreviousValue(worldPosition);
            m_DeltaScreenPosition = screenPosition - this.GetPreviousValue(screenPosition);

            m_IsDoubleClick0 = false;
            m_IsDoubleClick1 = false;

            if (button0Down)
            {
                if (Time.realtimeSinceStartup - this.GetPreviousValue(Time.realtimeSinceStartup) < 0.3f)
                {
                    m_IsDoubleClick0 = true;
                }
            }

            if (button1Down)
            {
                if (Time.realtimeSinceStartup - this.GetPreviousValue(Time.realtimeSinceStartup) < 0.3f)
                {
                    m_IsDoubleClick1 = true;
                }
            }
        }

        public static Vector3 screenPosition
        {
            get
            {
                return Input.mousePosition;
            }
        }
        public static Vector3 screenPositionRelativeCenter
        {
            get
            {
                return screenPosition - new Vector3(Screen.width, Screen.height) / 2.0f;
            }
        }

        public static Vector3 worldPosition
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(screenPosition);
            }
        }

        public static Vector3 deltaWorldPosition => instance.m_DeltaWorldPosition;
        public static Vector3 deltaScreenPosition => instance.m_DeltaScreenPosition;

        public static bool isDoubleClick0 => instance.m_IsDoubleClick0;
        public static bool isDoubleClick1 => instance.m_IsDoubleClick1;

        public static float scroll => Input.mouseScrollDelta.y;

        public static bool button0 => Input.GetMouseButton(0);
        public static bool button0Up => Input.GetMouseButtonUp(0);
        public static bool button0Down => Input.GetMouseButtonDown(0);

        public static bool button1 => Input.GetMouseButton(1);
        public static bool button1Up => Input.GetMouseButtonUp(1);
        public static bool button1Down => Input.GetMouseButtonDown(1);

        public static bool button2 => Input.GetMouseButton(2);
        public static bool button2Up => Input.GetMouseButtonUp(2);
        public static bool button2Down => Input.GetMouseButtonDown(2);

        public static bool touch0
        {
            get
            {
                return Input.GetTouch(0).phase == TouchPhase.Stationary
                    || Input.GetTouch(0).phase == TouchPhase.Moved;
            }
        }
        public static bool touch0Up
        {
            get
            {
                return Input.GetTouch(0).phase == TouchPhase.Ended;
            }
        }
        public static bool touch0Down
        {
            get
            {
                return Input.GetTouch(0).phase == TouchPhase.Began;
            }
        }
    }
}
