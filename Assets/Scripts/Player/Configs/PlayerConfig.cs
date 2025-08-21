using UnityEngine;
[CreateAssetMenu(menuName = "Configs/PlayerConfig", fileName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [System.Serializable]
    public class SceneSpawn
    {
        public string sceneName;
        public Vector3 spawnPosition;
    }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private SceneSpawn[] sceneSpawns;

    public GameObject PlayerPrefab => playerPrefab;

    public Vector3 GetSpawnPosition(string currentScene)
    {
        foreach (var spawn in sceneSpawns)
        {
            if (spawn.sceneName == currentScene)
                return spawn.spawnPosition;
        }
        Debug.LogWarning($"No spawn config found for {currentScene}");
        return Vector3.zero;
    }
}