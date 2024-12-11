using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;
    using Object = UnityEngine.Object;

    static class DeveloperTools
    {
        #region Other
        [MenuItem("MaxTools/Make Screenshot %&s", priority = 15)]
        static void MakeScreenshot()
        {
            var directoryName = "Screenshots";

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            var data = DateTime.Now.ToString().Replace(":", "-");
            var resolution = $"{Screen.width}x{Screen.height}";
            var path = $"{directoryName}/{data}_{resolution}.png";

            ScreenCapture.CaptureScreenshot(path);

            Debug.Log("Screenshot has been created!");
        }

        [MenuItem("MaxTools/Editor Break %&d", priority = 15)]
        static void EditorBreak()
        {
            Debug.Break();

            Debug.Log("Editor break!");
        }

        [MenuItem("MaxTools/Clear All EditorPrefs", priority = 30)]
        static void ClearAllEditorPrefs()
        {
            EditorPrefs.DeleteAll();

            Debug.Log("EditorPrefs has been cleared!");
        }

        [MenuItem("MaxTools/Clear All PlayerPrefs", priority = 30)]
        static void ClearAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("PlayerPrefs has been cleared!");
        }

        [MenuItem("MaxTools/Set Position (0,0,0) [World] #0", priority = 45)]
        static void SetPositionZeroWorld()
        {
            foreach (var transform in Selection.transforms)
            {
                Undo.RecordObject(transform, "Set Position (0,0,0) [World]");

                transform.position = Vector3.zero;
            }
        }

        [MenuItem("MaxTools/Set Position (0,0,0) [World] #0", true)]
        static bool SetPositionZeroWorld_Validate()
        {
            return Selection.transforms != null && Selection.transforms.Length > 0;
        }

        [MenuItem("MaxTools/Set Position (0,0,0) [Local] &0", priority = 45)]
        static void SetPositionZeroLocal()
        {
            foreach (var transform in Selection.transforms)
            {
                Undo.RecordObject(transform, "Set Position (0,0,0) [Local]");

                transform.localPosition = Vector3.zero;
            }
        }

        [MenuItem("MaxTools/Set Position (0,0,0) [Local] &0", true)]
        static bool SetPositionZeroLocal_Validate()
        {
            return Selection.transforms != null && Selection.transforms.Length > 0;
        }
        #endregion

        #region Game Speed
        [MenuItem("MaxTools/Game Speed/x0", priority = 60)]
        static void GameSpeed_x0()
        {
            Time.timeScale = 0.0f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F0")}");
        }

        [MenuItem("MaxTools/Game Speed/x0.10", priority = 60)]
        static void GameSpeed_x010()
        {
            Time.timeScale = 0.10f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F2")}");
        }

        [MenuItem("MaxTools/Game Speed/x0.25", priority = 60)]
        static void GameSpeed_x025()
        {
            Time.timeScale = 0.25f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F2")}");
        }

        [MenuItem("MaxTools/Game Speed/x0.50", priority = 60)]
        static void GameSpeed_x050()
        {
            Time.timeScale = 0.50f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F2")}");
        }

        [MenuItem("MaxTools/Game Speed/x1", priority = 60)]
        static void GameSpeed_x1()
        {
            Time.timeScale = 1.0f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F0")}");
        }

        [MenuItem("MaxTools/Game Speed/x5", priority = 60)]
        static void GameSpeed_x5()
        {
            Time.timeScale = 5.0f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F0")}");
        }

        [MenuItem("MaxTools/Game Speed/x10", priority = 60)]
        static void GameSpeed_x10()
        {
            Time.timeScale = 10.0f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F0")}");
        }

        [MenuItem("MaxTools/Game Speed/x25", priority = 60)]
        static void GameSpeed_x25()
        {
            Time.timeScale = 25.0f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F0")}");
        }

        [MenuItem("MaxTools/Game Speed/x50", priority = 60)]
        static void GameSpeed_x50()
        {
            Time.timeScale = 50.0f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F0")}");
        }

        [MenuItem("MaxTools/Game Speed/x99", priority = 60)]
        static void GameSpeed_x99()
        {
            Time.timeScale = 99.0f;

            Debug.Log($"Time.timeScale: {Time.timeScale.ToString("F0")}");
        }
        #endregion

        #region Find References
        [MenuItem("MaxTools/Find References In Scenes", priority = 75)]
        static void FindReferencesInScenes()
        {
            Debug.Log("--- Start Find References In Scenes ---");

            if (Selection.activeObject is MonoScript monoScript)
            {
                if (monoScript.GetClass().IsSubclassOf(typeof(Component)))
                {
                    for (int i = 0; i < SceneManager.sceneCount; ++i)
                    {
                        foreach (var root in SceneManager.GetSceneAt(i).GetRootGameObjects())
                        {
                            foreach (var component in root.GetComponentsInChildren(monoScript.GetClass(), true))
                            {
                                Debug.Log($"obj:{component.name}", component);
                            }
                        }
                    }
                }
            }
            else
            {
                var whatObjects = new Object[] { Selection.activeObject };

                if (Selection.activeObject is GameObject whatGameObject)
                {
                    var whatComponents = whatGameObject.GetComponents<Component>();

                    whatObjects = whatObjects.Concat(whatComponents).ToArray();
                }

                var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();

                if (prefabStage != null)
                {
                    FindReferenceCompare(whatObjects, prefabStage.prefabContentsRoot.GetComponentsInChildren<Component>(true));
                }
                else
                {
                    for (int i = 0; i < SceneManager.sceneCount; ++i)
                    {
                        foreach (var root in SceneManager.GetSceneAt(i).GetRootGameObjects())
                        {
                            FindReferenceCompare(whatObjects, root.GetComponentsInChildren<Component>(true));
                        }
                    }
                }
            }

            Debug.Log("--- End Find References In Scenes ---");
        }

        [MenuItem("MaxTools/Find References In Scenes", true)]
        static bool FindReferencesInScenes_Validate()
        {
            return Selection.activeObject != null;
        }

        [MenuItem("MaxTools/Find References In Assets", priority = 75)]
        static void FindReferencesInAssets()
        {
            Debug.Log("--- Start Find References In Assets ---");

            if (Selection.activeObject is MonoScript monoScript)
            {
                foreach (var path in AssetDatabase.GetAllAssetPaths())
                {
                    if (!path.StartsWith("Assets"))
                    {
                        continue;
                    }

                    var load = AssetDatabase.LoadAssetAtPath<Object>(path);

                    switch (load)
                    {
                        case GameObject whereGameObject:
                            {
                                if (monoScript.GetClass().IsSubclassOf(typeof(Component)))
                                {
                                    foreach (var component in whereGameObject.GetComponentsInChildren(monoScript.GetClass(), true))
                                    {
                                        Debug.Log($"obj:{component.name}", whereGameObject);
                                    }
                                }
                            }
                            break;

                        case ScriptableObject whereScriptableObject:
                            {
                                if (whereScriptableObject.GetType() == monoScript.GetClass())
                                {
                                    Debug.Log($"obj:{whereScriptableObject.name}", whereScriptableObject);
                                }
                            }
                            break;
                    }
                }
            }
            else
            {
                var whatObjects = new Object[] { Selection.activeObject };

                if (Selection.activeObject is GameObject whatGameObject)
                {
                    var whatComponents = whatGameObject.GetComponents<Component>();

                    whatObjects = whatObjects.Concat(whatComponents).ToArray();
                }

                foreach (var path in AssetDatabase.GetAllAssetPaths())
                {
                    if (!path.StartsWith("Assets"))
                    {
                        continue;
                    }

                    var load = AssetDatabase.LoadAssetAtPath<Object>(path);

                    switch (load)
                    {
                        case GameObject whereGameObject:
                            FindReferenceCompare(whatObjects, whereGameObject.GetComponentsInChildren<Component>(true));
                            break;

                        case ScriptableObject whereScriptableObject:
                            FindReferenceCompare(whatObjects, new Object[] { whereScriptableObject });
                            break;
                    }
                }
            }

            Debug.Log("--- End Find References In Assets ---");
        }

        [MenuItem("MaxTools/Find References In Assets", true)]
        static bool FindReferencesInAssets_Validate()
        {
            return Selection.activeObject != null && AssetDatabase.Contains(Selection.activeObject);
        }

        static void FindReferenceCompare(Object[] whatObjects, Object[] whereObjects)
        {
            foreach (var whatObject in whatObjects)
            {
                if (whatObject == null)
                {
                    continue;
                }

                foreach (var whereObject in whereObjects)
                {
                    if (whereObject == null)
                    {
                        continue;
                    }

                    var iterator = new SerializedObject(whereObject).GetIterator();

                    while (iterator.NextVisible(true))
                    {
                        if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            if (iterator.objectReferenceValue == whatObject)
                            {
                                FindReferenceShowLog(whatObject, whereObject, iterator);
                            }
                        }
                    }
                }
            }
        }
        static void FindReferenceShowLog(Object whatObject, Object whereObject, SerializedProperty property)
        {
            var whereFormatted = $"obj: {whereObject.name} ({whereObject.GetType().Name})";
            var propertyFormatted = $"var: {property.GetPropertyPath()} ({whatObject.GetType().Name})";

            switch (whereObject)
            {
                case Component whereComponent:
                    {
                        if (PrefabUtility.IsPartOfPrefabAsset(whereComponent))
                        {
                            Debug.Log($"{whereFormatted} -> {propertyFormatted}",
                                whereComponent.transform.root.gameObject);
                            break;
                        }
                        else
                        {
                            Debug.Log($"{whereFormatted} -> {propertyFormatted}",
                                whereComponent.gameObject);
                            break;
                        }
                    }

                case ScriptableObject whereScriptableObject:
                    {
                        Debug.Log($"{whereFormatted} -> {propertyFormatted}",
                            whereScriptableObject);
                        break;
                    }
            }
        }
        #endregion

        #region Target Frame Rate
        [MenuItem("MaxTools/Set Target Frame Rate/30", priority = 105)]
        static void SetTargetFrameRate30()
        {
            Application.targetFrameRate = 30;
        }

        [MenuItem("MaxTools/Set Target Frame Rate/60", priority = 105)]
        static void SetTargetFrameRate60()
        {
            Application.targetFrameRate = 60;
        }

        [MenuItem("MaxTools/Set Target Frame Rate/100", priority = 105)]
        static void SetTargetFrameRate100()
        {
            Application.targetFrameRate = 100;
        }

        [MenuItem("MaxTools/Set Target Frame Rate/999", priority = 105)]
        static void SetTargetFrameRate999()
        {
            Application.targetFrameRate = 999;
        }
        #endregion

        #region Injected Log
        [MenuItem("MaxTools/Add Injected Log", priority = 120)]
        static void AddInjectedLog()
        {
            if (EditorUtility.DisplayDialog("Add InjectedLog()", "Are you sure?", "Yes", "Cancel"))
            {
                foreach (var _object in Selection.objects)
                {
                    var objectPath = AssetDatabase.GetAssetPath(_object);

                    foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
                    {
                        if (assetPath.StartsWith(objectPath) && assetPath.EndsWith(".cs"))
                        {
                            var text = File.ReadAllText(assetPath, Encoding.UTF8);

                            text = text.Replace("{", "{MaxTools.Tools.InjectedLog();");

                            File.WriteAllText(assetPath, text, Encoding.UTF8);
                        }
                    }
                }
            }
        }

        [MenuItem("MaxTools/Add Injected Log", true)]
        static bool AddInjectedLog_Validate()
        {
            if (Selection.objects == null || Selection.objects.Length == 0)
            {
                return false;
            }

            foreach (var _object in Selection.objects)
            {
                if (_object == null || !AssetDatabase.Contains(_object))
                {
                    return false;
                }
            }

            return true;
        }

        [MenuItem("MaxTools/Remove Injected Log", priority = 120)]
        static void RemoveInjectedLog()
        {
            foreach (var _object in Selection.objects)
            {
                var objectPath = AssetDatabase.GetAssetPath(_object);

                foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
                {
                    if (assetPath.StartsWith(objectPath) && assetPath.EndsWith(".cs"))
                    {
                        var text = File.ReadAllText(assetPath, Encoding.UTF8);

                        text = text.Replace("{MaxTools.Tools.InjectedLog();", "{");

                        File.WriteAllText(assetPath, text, Encoding.UTF8);
                    }
                }
            }
        }

        [MenuItem("MaxTools/Remove Injected Log", true)]
        static bool RemoveInjectedLog_Validate()
        {
            if (Selection.objects == null || Selection.objects.Length == 0)
            {
                return false;
            }

            foreach (var _object in Selection.objects)
            {
                if (_object == null || !AssetDatabase.Contains(_object))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Other
        [MenuItem("MaxTools/Script Space Remover", priority = 135)]
        static void ScriptSpaceRemover()
        {
            foreach (var path in AssetDatabase.GetAllAssetPaths())
            {
                if (path.StartsWith("Assets") && path.EndsWith(".cs"))
                {
                    var lines = new List<string>(File.ReadAllLines(path, Encoding.UTF8));

                    for (int i = 0; i < lines.Count; ++i)
                    {
                        for (int j = lines[i].Length - 1; j >= 0; --j)
                        {
                            if (!char.IsWhiteSpace(lines[i][j]))
                            {
                                lines[i] = lines[i].Substring(0, j + 1);

                                break;
                            }

                            if (j == 0)
                            {
                                lines[i] = string.Empty;
                            }
                        }
                    }

                    while (lines.Count > 0)
                    {
                        if (lines[0].Length == 0)
                        {
                            lines.RemoveAt(0);
                        }
                        else
                            break;
                    }

                    while (lines.Count > 0)
                    {
                        if (lines[lines.Count - 1].Length == 0)
                        {
                            lines.RemoveAt(lines.Count - 1);
                        }
                        else
                            break;
                    }

                    File.WriteAllLines(path, lines, Encoding.UTF8);
                }
            }
        }

        [MenuItem("MaxTools/Missing/Remove Missing Scripts In Scenes", priority = 150)]
        static void RemoveMissingScriptsInScenes()
        {
            if (EditorUtility.DisplayDialog("Remove Missing Scripts In Scenes", "Are you sure?", "Yes", "Cancel"))
            {
                Debug.Log("--- Start Remove Missing Scripts In Scenes ---");

                for (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    foreach (var root in SceneManager.GetSceneAt(i).GetRootGameObjects())
                    {
                        foreach (var transform in root.GetComponentsInChildren<Transform>(true))
                        {
                            int counter = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(transform.gameObject);

                            if (counter > 0)
                            {
                                Debug.Log($"{transform.name} {counter}", transform.gameObject);
                            }
                        }
                    }
                }

                Debug.Log("--- End Remove Missing Scripts In Scenes ---");
            }
        }

        [MenuItem("MaxTools/Missing/Remove Missing Scripts In Assets", priority = 150)]
        static void RemoveMissingScriptsInAssets()
        {
            if (EditorUtility.DisplayDialog("Remove Missing Scripts In Assets", "Are you sure?", "Yes", "Cancel"))
            {
                Debug.Log("--- Start Remove Missing Scripts In Assets ---");

                foreach (var assetGuid in AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets" }))
                {
                    try
                    {
                        var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                        var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                        var prefab = PrefabUtility.LoadPrefabContents(assetPath);

                        foreach (var transform in prefab.GetComponentsInChildren<Transform>(true))
                        {
                            int counter = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(transform.gameObject);

                            if (counter > 0)
                            {
                                Debug.Log($"{transform.name} {counter}", gameObject);
                            }
                        }

                        PrefabUtility.SaveAsPrefabAsset(prefab, assetPath);
                        PrefabUtility.UnloadPrefabContents(prefab);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                    }
                }

                Debug.Log("--- End Remove Missing Scripts In Assets ---");
            }
        }

        [MenuItem("MaxTools/Missing/Find Missing References In Scenes", priority = 150)]
        static void FindMissingReferencesInScenes()
        {
            Debug.Log("--- Start Find Missing References In Scenes ---");

            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                foreach (var root in SceneManager.GetSceneAt(i).GetRootGameObjects())
                {
                    foreach (var component in root.GetComponentsInChildren<Component>(true))
                    {
                        try
                        {
                            if (component == null)
                            {
                                continue;
                            }

                            var serializedObject = new SerializedObject(component);

                            var iterator = serializedObject.GetIterator();

                            while (iterator.NextVisible(true))
                            {
                                if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                                {
                                    if (iterator.objectReferenceValue == null && iterator.objectReferenceInstanceIDValue != 0)
                                    {
                                        Debug.Log(
                                            $"obj: {component.name} ({component.GetType().Name}) ->" + " " +
                                            $"var: {iterator.GetPropertyPath()}",
                                            component.gameObject);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError(ex);
                        }
                    }
                }
            }

            Debug.Log("--- End Find Missing References In Scenes ---");
        }

        [MenuItem("MaxTools/Missing/Find Missing References In Assets", priority = 150)]
        static void FindMissingReferencesInAssets()
        {
            Debug.Log("--- Start Find Missing References In Assets ---");

            foreach (var assetGuid in AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets" }))
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                foreach (var component in gameObject.GetComponentsInChildren<Component>(true))
                {
                    try
                    {
                        if (component == null)
                        {
                            continue;
                        }

                        var serializedObject = new SerializedObject(component);

                        var iterator = serializedObject.GetIterator();

                        while (iterator.NextVisible(true))
                        {
                            if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                            {
                                if (iterator.objectReferenceValue == null && iterator.objectReferenceInstanceIDValue != 0)
                                {
                                    Debug.Log(
                                        $"obj: {component.name} ({component.GetType().Name}) ->" + " " +
                                        $"var: {iterator.GetPropertyPath()}",
                                        gameObject);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                    }
                }
            }

            Debug.Log("--- End Find Missing References In Assets ---");
        }
        #endregion
    }
}
