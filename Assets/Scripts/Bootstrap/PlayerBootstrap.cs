using UnityEngine;
using Zenject;

public class PlayerBootstrap : MonoBehaviour
{
    private IPlayerProvider _playerProvider;

    [Inject]
    public void Construct(IPlayerProvider playerProvider)
    {
        _playerProvider = playerProvider;
    }

    private void Start()
    {
        _playerProvider.SpawnPlayer();
    }
}