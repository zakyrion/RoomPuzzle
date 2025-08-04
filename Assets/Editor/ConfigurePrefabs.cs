using UnityEngine;
using UnityEditor;
using RoomPuzzle;
public class ConfigurePrefabs
{
    private const string PrefabPath = "Assets/Resources/Prefabs/";
    [MenuItem("Tools/Project Setup/2. Configure Prefabs")]
    public static void RunConfiguration()
    {
        if (!ConfigureKeyPrefab()) return;
        if (!ConfigureDoorPrefab()) return;
        if (!ConfigurePlayerPrefab()) return;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Prefabs Configured",
            "All prefabs in 'Resources/Prefabs' have been successfully configured with the necessary components.", "OK");
    }
    private static bool ConfigureKeyPrefab()
    {
        string path = PrefabPath + "Key.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) { Debug.LogError($"Could not find Key prefab at path: {path}"); return false; }
        if (prefab.GetComponent<KeyView>() == null) prefab.AddComponent<KeyView>();
        if (prefab.GetComponent<KeyInstaller>() == null) prefab.AddComponent<KeyInstaller>();
        EditorUtility.SetDirty(prefab);
        PrefabUtility.SavePrefabAsset(prefab);
        Debug.Log($"Configured: {prefab.name}");
        return true;
    }
    private static bool ConfigureDoorPrefab()
    {
        string path = PrefabPath + "Door.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) { Debug.LogError($"Could not find Door prefab at path: {path}"); return false; }
        DoorView doorView = prefab.GetComponent<DoorView>() ?? prefab.AddComponent<DoorView>();
        if (prefab.GetComponent<DoorInstaller>() == null) prefab.AddComponent<DoorInstaller>();
        Transform visualTransform = prefab.transform.Find("Visual");
        if (visualTransform != null)
        {
            SerializedObject so = new SerializedObject(doorView);
            so.FindProperty("_doorVisual").objectReferenceValue = visualTransform;
            so.ApplyModifiedProperties();
        }
        else { Debug.LogWarning($"Could not find 'Visual' child in {prefab.name}."); }
        EditorUtility.SetDirty(prefab);
        PrefabUtility.SavePrefabAsset(prefab);
        Debug.Log($"Configured: {prefab.name}");
        return true;
    }
    private static bool ConfigurePlayerPrefab()
    {
        string path = PrefabPath + "Player.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) { Debug.LogError($"Could not find Player prefab at path: {path}"); return false; }
        if (prefab.GetComponent<PlayerMovement>() == null) prefab.AddComponent<PlayerMovement>();
        if (prefab.GetComponent<PlayerInteraction>() == null) prefab.AddComponent<PlayerInteraction>();
        if (prefab.GetComponent<PlayerInstaller>() == null) prefab.AddComponent<PlayerInstaller>();
        EditorUtility.SetDirty(prefab);
        PrefabUtility.SavePrefabAsset(prefab);
        Debug.Log($"Configured: {prefab.name}");
        return true;
    }
}