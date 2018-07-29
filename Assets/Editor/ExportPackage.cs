using UnityEngine;
using UnityEditor;

public class ExportPackage
{
    private readonly static string[] Paths = {
        "Assets/Plugins/SystemVolumePlugin",
    };

    [MenuItem("Assets/Export SystemVolumePlugin")]
    private static void Export()
    {
        AssetDatabase.ExportPackage(Paths, "SystemVolumePlugin.unitypackage", ExportPackageOptions.Recurse);
        Debug.Log("Export complete!");
    }
}
