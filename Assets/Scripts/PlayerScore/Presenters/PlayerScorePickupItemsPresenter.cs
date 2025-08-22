using System.Collections.Generic;
using PlayerScore.Models;
using PlayerScore.Providers;
using PlayerScore.Views;

namespace PlayerScore.Presenters
{
    public class PlayerScorePickupItemsPresenter : IPlayerScorePickupItemsPresenter
    {
        private readonly List<IPlayerScorePickupItemView> _pickupItemViews = new();
        private readonly IPlayerScoreModel _playerScoreModel;
        private readonly IPlayerScoreProvider _playerScoreProvider;

        public PlayerScorePickupItemsPresenter(IPlayerScoreProvider playerScoreProvider, IPlayerScoreModel playerScoreModel)
        {
            _playerScoreProvider = playerScoreProvider;
            _playerScoreModel = playerScoreModel;
        }

        public void Dispose()
        {
            foreach (var itemView in _pickupItemViews)
            {
                _playerScoreProvider.DisposePickupItem(itemView);
            }
        }

        public void Initialize()
        {
            var config = _playerScoreModel.GetConfig();
            var spawnData = config.GetSpawnData();
            foreach (var data in spawnData)
            {
                var itemView = _playerScoreProvider.GetPickupItem(data.Type);
                itemView.Initialize(data, config.PlayerTag);
                itemView.OnPickup += ItemViewPickupHandler;
                _pickupItemViews.Add(itemView);
            }
        }

        private void ItemViewPickupHandler(IPlayerScorePickupItemView itemView)
        {
            itemView.OnPickup -= ItemViewPickupHandler;
            _playerScoreModel.AddScore(itemView.Score);
            _pickupItemViews.Remove(itemView);
            _playerScoreProvider.DisposePickupItem(itemView);
        }
    }
}
