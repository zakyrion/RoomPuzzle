using System.Collections.Generic;
using Inventory.Configs;
using Inventory.Models;
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
        private readonly IInventoryModel _inventoryModel;

        public PlayerScorePickupItemsPresenter(
            IPlayerScoreProvider playerScoreProvider, 
            IPlayerScoreModel playerScoreModel,
            IInventoryModel inventoryModel)
        {
            _playerScoreProvider = playerScoreProvider;
            _playerScoreModel = playerScoreModel;
            _inventoryModel = inventoryModel;
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
            // Check if inventory has free slot before picking up
            if (!_inventoryModel.HasFreeSlot())
            {
                // Inventory is full, don't pickup the item
                return;
            }

            itemView.OnPickup -= ItemViewPickupHandler;
            
            // Add to score (existing behavior)
            _playerScoreModel.AddScore(itemView.Score);
            
            // Add to inventory (new behavior)
            var inventoryItem = new InventoryItem(
                $"Item {itemView.Type}",
                itemView.Type,
                itemView.Score
            );
            _inventoryModel.AddItem(inventoryItem);
            
            _pickupItemViews.Remove(itemView);
            _playerScoreProvider.DisposePickupItem(itemView);
        }
    }
}