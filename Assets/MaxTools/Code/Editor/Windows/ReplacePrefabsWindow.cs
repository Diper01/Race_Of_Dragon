using UnityEditor;
using UnityEngine;
using System.Linq;

namespace MaxTools.Editor
{
    public class ReplacePrefabsWindow : EditorWindow
    {
        int oldSize;
        Object[] oldPrefabs;
        Object newPrefab;
        Vector2 scrollPosition;

        [MenuItem("MaxTools/Replace Prefabs", priority = 90)]
        static void ShowWindow()
        {
            var window = GetWindow<ReplacePrefabsWindow>("Replace Prefabs");

            window.minSize = window.maxSize = new Vector2(400.0f, 170.0f);
        }

        void OnGUI()
        {
            System.Array.Resize(ref oldPrefabs, oldSize = EditorGUILayout.DelayedIntField("Old Size", oldSize));

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Height(100));

            EditorGUI.indentLevel++;

            for (int i = 0; i < oldSize; ++i)
            {
                oldPrefabs[i] = EditorGUILayout.ObjectField($"Old Prefab {i}", oldPrefabs[i], typeof(Object), false);
            }

            EditorGUI.indentLevel--;

            GUILayout.EndScrollView();

            GUILayout.FlexibleSpace();

            newPrefab = EditorGUILayout.ObjectField("New Prefab", newPrefab, typeof(Object), false);

            if (GUILayout.Button("Replace"))
            {
                foreach (var oldTransform in Selection.objects.Select((o) => (o as GameObject).transform))
                {
                    for (int i = 0; i < oldSize; ++i)
                    {
                        if (PrefabUtility.GetCorrespondingObjectFromSource(oldTransform.gameObject) == oldPrefabs[i])
                        {
                            var newObject = PrefabUtility.InstantiatePrefab(newPrefab) as GameObject;

                            Undo.RegisterCreatedObjectUndo(newObject, "[New Object] Created");

                            newObject.transform.SetPositionAndRotation(oldTransform.position, oldTransform.rotation);
                            newObject.transform.localScale = oldTransform.lossyScale;
                            newObject.transform.parent = oldTransform.parent;
                            newObject.transform.SetSiblingIndex(oldTransform.GetSiblingIndex());

                            Undo.DestroyObjectImmediate(oldTransform.gameObject);

                            break;
                        }
                    }
                }
            }
        }
    }
}
