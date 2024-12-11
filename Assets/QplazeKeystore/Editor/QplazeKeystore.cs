using UnityEditor;
using UnityEngine;

namespace Qplaze
{
    [CreateAssetMenu(fileName = "QplazeKeystore", menuName = "Qplaze/QplazeKeystore")]
    public class QplazeKeystore : ScriptableObject
    {
        public Object keystoreFile;
        public string keystorePass;
        public string keyaliasName;
        public string keyaliasPass;

        [InitializeOnLoadMethod]
        static void Initialize()
        {
            foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
            {
                if (!assetPath.StartsWith("Assets"))
                {
                    continue;
                }

                if (AssetDatabase.LoadAssetAtPath<Object>(assetPath) is QplazeKeystore keystore)
                {
                    if (keystore.keystoreFile != null)
                    {
#if UNITY_2019_2_OR_NEWER
                        PlayerSettings.Android.useCustomKeystore = true;
#endif
                        PlayerSettings.Android.keystoreName = AssetDatabase.GetAssetPath(keystore.keystoreFile);
                        PlayerSettings.Android.keystorePass = keystore.keystorePass;
                        PlayerSettings.Android.keyaliasName = keystore.keyaliasName;
                        PlayerSettings.Android.keyaliasPass = keystore.keyaliasPass;
                        break;
                    }
                }
            }
        }
    }
}
