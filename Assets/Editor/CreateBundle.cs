using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using Db = UnityEngine.Debug;

public class CreateBundle : MonoBehaviour
{
    const string mainAppPath = "RoomArrangerVR/Furniture";
    static Regex prefabItemPattern = new Regex(@"/([^/]+)\.prefab$");

    [MenuItem("Bundle Creator/Create Bundles in Room Arranger VR Directory %b")]
    static void BuildBundlesDirectly()
    {
        BuildBundlesInPath(Application.persistentDataPath.TrimEnd(Application.productName.ToCharArray()) + mainAppPath);
    }

    [MenuItem("Bundle Creator/Create Bundles in Assets")]
    static void BuildBundlesInAssets()
    {
        BuildBundlesInPath("Assets/Bundles/");
    }

    [MenuItem("Bundle Creator/Open Room Arranger VR Directory")]
    static void OpenBundlesDirectory()
    {
        try
        {
            Process.Start(Application.persistentDataPath.TrimEnd(Application.productName.ToCharArray()) + mainAppPath);
        }
        catch (Win32Exception win32Exception)
        {
            //The system cannot find the file specified...
            Db.Log(win32Exception.Message);
        }
    }

    static void BuildBundlesInPath(string path)
    {
        AssignThumbnails();

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        Directory.CreateDirectory(path);

        UnityEngine.Debug.Log("Creating bundles for Windows 64");
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        RemoveSpacesInFileNames(path);

        AssetDatabase.Refresh();
        UnityEngine.Debug.Log("Process complete!");
    }

    static void AssignThumbnails()
    {
        if (!Directory.Exists("Assets/Thumbnails/"))
        {
            return;
        }
        foreach (var bundleName in AssetDatabase.GetAllAssetBundleNames())
        {
            string[] paths = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
            foreach (var path in paths)
            {
                var match = prefabItemPattern.Match(path);
                if(match.Success)
                {
                    string imagePath = "Assets/Thumbnails/" + match.Groups[1].Value + ".png";
                    var item = AssetImporter.GetAtPath(imagePath);
                    if (item != null)
                    {
                        //Db.Log("Assigned " + imagePath + " to bundle " + bundleName);
                        item.assetBundleName = bundleName;
                    }
                    else
                    {
                        Db.LogWarning("No " + imagePath + " file found. Bundle item will have default thumbnail.");
                    }
                }
            }
        }
        Db.Log("Assigning thumbnails to bundles DONE");
    }

    static void RemoveSpacesInFileNames(string path)
    {
        foreach (var name in Directory.GetFiles(path))
        {
            string oldName = name;
            string newName = name.Replace(' ', '-');
            File.Move(oldName, newName);
        }
    }
}