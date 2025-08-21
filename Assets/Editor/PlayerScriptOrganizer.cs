using UnityEditor;
using UnityEngine;
using System.IO;

public static class PlayerScriptOrganizer
{
    [MenuItem("Tools/Organize Player Scripts")]
    public static void Organize()
    {
        string basePath = "Assets/Scripts/Player";

        // Ensure destination folders exist
        Directory.CreateDirectory(Path.Combine(basePath, "Animator/Model"));
        Directory.CreateDirectory(Path.Combine(basePath, "Animator/View"));
        Directory.CreateDirectory(Path.Combine(basePath, "Animator/Presenter"));

        // Move files into proper folders
        MoveFile(basePath, "IPlayerAnimatorModel.cs", "Animator/Model");
        MoveFile(basePath, "PlayerAnimatorModel.cs", "Animator/Model");

        MoveFile(basePath, "IPlayerAnimatorView.cs", "Animator/View");
        MoveFile(basePath, "PlayerAnimatorView.cs", "Animator/View");

        MoveFile(basePath, "IPlayerAnimatorPresenter.cs", "Animator/Presenter");
        MoveFile(basePath, "PlayerAnimatorPresenter.cs", "Animator/Presenter");

        MoveFile(basePath, "PlayerAnimatorInstaller.cs", "Animator");

        // Clean empty dirs
        CleanEmptyDirs(basePath);

        AssetDatabase.Refresh();
        Debug.Log("✅ Player scripts reorganized successfully.");
    }

    private static void MoveFile(string basePath, string fileName, string subFolder)
    {
        string src = Path.Combine(basePath, fileName);
        string dstDir = Path.Combine(basePath, subFolder);
        string dst = Path.Combine(dstDir, fileName);

        if (File.Exists(src))
        {
            if (File.Exists(dst))
            {
                Debug.LogWarning($"⚠️ Destination already has {fileName}, skipping.");
                return;
            }
            File.Move(src, dst);
            File.Move(src + ".meta", dst + ".meta");
            Debug.Log($"Moved {fileName} → {subFolder}");
        }
    }

    private static void CleanEmptyDirs(string path)
    {
        foreach (var dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
        {
            if (Directory.GetFiles(dir).Length == 0 && Directory.GetDirectories(dir).Length == 0)
            {
                Directory.Delete(dir, false);
                string metaFile = dir + ".meta";
                if (File.Exists(metaFile)) File.Delete(metaFile);
                Debug.Log($"Deleted empty folder: {dir}");
            }
        }
    }
}