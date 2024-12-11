using UnityEditor;
using System.IO;

namespace MaxTools.Editor
{
    public class LocalizationAssetPostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var assetPath in importedAssets)
            {
                if (Check(assetPath))
                {
                    Localization.LoadLanguageSystem();

                    return;
                }
            }

            foreach (var assetPath in deletedAssets)
            {
                if (Check(assetPath))
                {
                    Localization.LoadLanguageSystem();

                    return;
                }
            }

            foreach (var assetPath in movedAssets)
            {
                if (Check(assetPath))
                {
                    Localization.LoadLanguageSystem();

                    return;
                }
            }
        }

        static bool Check(string assetPath)
        {
            var directoryName = Path.GetDirectoryName(assetPath);

            return directoryName.EndsWith(@"\Resources\Localization");
        }
    }
}
