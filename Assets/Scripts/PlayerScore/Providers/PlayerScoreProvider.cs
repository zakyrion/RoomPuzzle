using JetBrains.Annotations;
using MainCanvas.Providers;
using PlayerScore.Configs;
using PlayerScore.UIs;
using PlayerScore.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PlayerScore.Providers
{
    [UsedImplicitly]
    public class PlayerScoreProvider : IPlayerScoreProvider
    {
        private const string PLAYER_SCORE_UI_PATH = "Prefabs/UI/PlayerScoreUI";
        private readonly IPlayerScoreProviderConfig _config;
        private readonly IMainCanvasProvider _mainCanvasProvider;
        private GameObject _playerScoreGameObject;
        private IPlayerScoreUI _playerScoreUI;

        public PlayerScoreProvider(IPlayerScoreProviderConfig config, IMainCanvasProvider mainCanvasProvider)
        {
            _config = config;
            _mainCanvasProvider = mainCanvasProvider;
        }

        public void DisposePickupItem(IPlayerScorePickupItemView view)
        {
            Object.Destroy(view.GameObject);
        }

        public void DisposePlayerScoreUI()
        {
            if (_playerScoreGameObject != null)
            {
                Object.Destroy(_playerScoreGameObject);
                _playerScoreGameObject = null;
                _playerScoreUI = null;
            }
        }

        public IPlayerScorePickupItemView GetPickupItem(PlayerScorePickupItemType type)
        {
            var prefab = _config.GetPrefab(type);
            var gameObject = Object.Instantiate(prefab);
            return gameObject.GetComponent<PlayerScorePickupItemView>();
        }

        public IPlayerScoreUI GetPlayerScoreUI()
        {
            if (_playerScoreUI != null)
            {
                return _playerScoreUI;
            }

            _playerScoreGameObject = GameObject.Instantiate(Resources.Load<GameObject>(PLAYER_SCORE_UI_PATH), _mainCanvasProvider.GetCanvas().transform);
            _playerScoreUI = _playerScoreGameObject.GetComponent<IPlayerScoreUI>();
            return _playerScoreUI;
        }
    }
}
