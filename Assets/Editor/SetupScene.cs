using RoomPuzzle;
using UnityEditor;
using UnityEngine;
using Zenject;
// Your project's namespace

public class SetupScene
{
    [MenuItem("Tools/Project Setup/3. Setup Scene Contexts")]
    public static void RunSceneSetup()
    {
        // Ensure there's only one ProjectContext
        if (Object.FindObjectsOfType<ProjectContext>().Length == 0)
        {
            SetupProjectContext();
        }
        else
        {
            Debug.Log("ProjectContext already exists in the scene.");
        }
        // Ensure there's only one SceneContext
        if (Object.FindObjectsOfType<SceneContext>().Length == 0)
        {
            SetupSceneContext();
        }
        else
        {
            Debug.Log("SceneContext already exists in the scene.");
        }
        EditorUtility.DisplayDialog("Scene Setup Complete",
            "ProjectContext and SceneContext have been configured in the current scene.", "OK");
    }

    private static void SetupProjectContext()
    {
        // Find the GameSettingsInstaller asset
        var guids = AssetDatabase.FindAssets("t:GameSettingsInstaller");
        if (guids.Length == 0)
        {
            Debug.LogError("GameSettingsInstaller not found! Please create it via Create > Installers > GameSettingsInstaller.");
            return;
        }
        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        var settingsInstaller = AssetDatabase.LoadAssetAtPath<GameSettingsInstaller>(path);
        // Create ProjectContext GameObject
        var projectContextGO = new GameObject("ProjectContext");
        var projectContext = projectContextGO.AddComponent<ProjectContext>();
        // Use reflection to set the private list of installers
        var so = new SerializedObject(projectContext);
        var property = so.FindProperty("_scriptableObjectInstallers");
        property.ClearArray();
        property.InsertArrayElementAtIndex(0);
        property.GetArrayElementAtIndex(0).objectReferenceValue = settingsInstaller;
        so.ApplyModifiedProperties();
        Debug.Log("ProjectContext created and configured.");
    }

    private static void SetupSceneContext()
    {
        var sceneContextGO = new GameObject("SceneContext");
        var sceneContext = sceneContextGO.AddComponent<SceneContext>();
        // Add the GameSceneInstaller to the list of MonoInstallers
        // This is simpler as the list is public
        sceneContext.Installers = new MonoInstaller[]
        {
            sceneContextGO.AddComponent<GameSceneInstaller>()
        };
        Debug.Log("SceneContext created and configured.");
    }
}
