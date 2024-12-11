using UnityEngine;
using System.Reflection;

namespace MaxTools.Editor
{
    public class NiceMethodPack
    {
        public MethodInfo methodInfo;
        public EasyButtonAttribute easyButton;

        public HideInInspector hideInInspector;
        public ShowIfAttribute showIf;

        public string displayName;
    }
}
