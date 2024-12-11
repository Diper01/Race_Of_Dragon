using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    public class RenameObjectsWindow : EditorWindow
    {
        enum SelectionType
        {
            None,
            Assets,
            SceneObjects,
            Mixed
        }

        string newName = "";
        int iterator = 1;
        int startIndex = 0;

        [MenuItem("MaxTools/Rename Objects", priority = 90)]
        static void ShowWindow()
        {
            var window = GetWindow<RenameObjectsWindow>("Rename Objects");

            window.minSize = window.maxSize = new Vector2(400.0f, 100.0f);
        }

        SelectionType GetSelectionType()
        {
            var selectionType = SelectionType.None;

            foreach (var selectedObject in Selection.objects)
            {
                switch (selectionType)
                {
                    case SelectionType.None:
                        {
                            if (AssetDatabase.Contains(selectedObject))
                            {
                                selectionType = SelectionType.Assets;
                            }
                            else
                                selectionType = SelectionType.SceneObjects;
                        }
                        break;

                    case SelectionType.Assets:
                        {
                            if (!AssetDatabase.Contains(selectedObject))
                            {
                                return SelectionType.Mixed;
                            }
                        }
                        break;

                    case SelectionType.SceneObjects:
                        {
                            if (AssetDatabase.Contains(selectedObject))
                            {
                                return SelectionType.Mixed;
                            }
                        }
                        break;
                }
            }

            return selectionType;
        }

        void OnGUI()
        {
            newName = EditorGUILayout.TextField("New Name", newName);
            startIndex = EditorGUILayout.IntField("Start Index", startIndex);
            iterator = EditorGUILayout.IntField("Iterator", iterator);

            if (GUILayout.Button("Rename"))
            {
                switch (GetSelectionType())
                {
                    case SelectionType.Assets:
                        {
                            int i = startIndex;

                            foreach (var selectedObject in Selection.objects)
                            {
                                var path = AssetDatabase.GetAssetPath(selectedObject);

                                AssetDatabase.RenameAsset(path, $"{newName}{i}");

                                i += iterator;
                            }
                        }
                        break;

                    case SelectionType.SceneObjects:
                        {
                            Undo.RecordObjects(Selection.objects, "Renaming");

                            int i = startIndex;

                            foreach (var selectedObject in Selection.objects)
                            {
                                selectedObject.name = $"{newName}{i}";

                                i += iterator;
                            }
                        }
                        break;
                }
            }
        }
    }
}
