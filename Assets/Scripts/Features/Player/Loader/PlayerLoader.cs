// METADATA file_path: Assets/Scripts/Features/Player/Loader/PlayerLoader.cs
using RoomPuzzle.Features.Player.View;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.Features.Player.Loader
{
    public class PlayerLoader : IPlayerLoader
    {
        public const string PlayerPrefabPath = "Prefabs/Player/PlayerView"; // relative to Resources/
        private readonly DiContainer _container;

        public PlayerLoader(DiContainer container)
        {
            _container = container;
        }

        public IPlayerView Create(Vector3 position)
        {
            var prefab = Resources.Load<PlayerView>(PlayerPrefabPath);
            if (prefab == null)
            {
                Debug.LogError($"[PlayerLoader] PlayerView prefab not found at Resources path: {PlayerPrefabPath}");
                return null;
            }
            var view = _container.InstantiatePrefabForComponent<PlayerView>(
                prefab, position, Quaternion.identity, null);
            return view;
        }
    }
}
