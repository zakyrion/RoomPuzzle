using UnityEngine;
using UnityEngine.SceneManagement;

public interface IPlayerProvider
{
    GameObject SpawnPlayer();
}

public class PlayerProvider : IPlayerProvider
{
    private readonly PlayerConfig _config;

    public PlayerProvider(PlayerConfig config)
    {
        _config = config;
    }

    public GameObject SpawnPlayer()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Vector3 pos = _config.GetSpawnPosition(currentScene);
        return Object.Instantiate(_config.PlayerPrefab, pos, Quaternion.identity);
    }
}