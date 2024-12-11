using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    using Editor = UnityEditor.Editor;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class ObjectEditor : Editor
    {
        NiceGeneralPack generalPack = null;

        void OnEnable()
        {
            if (NiceInspector.HasNiceInspector(this))
            {
                generalPack = new NiceGeneralPack(this);
            }
        }

        public override void OnInspectorGUI()
        {
            if (NiceInspector.HasNiceInspector(this))
            {
                serializedObject.Update();

                serializedObject.SourceScriptLayout();

                NiceInspector.DrawNiceInspectorLayout(generalPack);

                serializedObject.ApplyModifiedProperties();
            }
            else
                DrawDefaultInspector();
        }
    }
}
