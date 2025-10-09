using System.Collections.Generic;
using Core.Presenters;
using PlayerScore.Models;
using PlayerScore.Providers;
using PlayerScore.Views;

namespace PlayerScore.Presenters
{
    public class PlayerScorePickupItemsPresenter : BasePresenter<IPlayerScoreModel, IPlayerScoreProvider, List<IPlayerScorePickupItemView>>, IPlayerScorePickupItemsPresenter
    {
        public PlayerScorePickupItemsPresenter(IPlayerScoreProvider playerScoreProvider, IPlayerScoreModel playerScoreModel)
            : base(playerScoreModel, playerScoreProvider)
        {
        }

        protected override List<IPlayerScorePickupItemView> GetViewFromProvider()
        {
            return new List<IPlayerScorePickupItemView>();
        }

        protected override void OnViewCreated()
        {
            var config = Model.GetConfig();
            var spawnData = config.GetSpawnData();
            
            foreach (var data in spawnData)
            {
                var itemView = Provider.GetPickupItem(data.Type);
                itemView.Initialize(data, config.PlayerTag);
                itemView.OnPickup += ItemViewPickupHandler;
                View.Add(itemView);
            }
        }

        protected override void OnBeforeDispose()
        {
            foreach (var itemView in View)
            {
                Provider.DisposePickupItem(itemView);
            }
        }

        private void ItemViewPickupHandler(IPlayerScorePickupItemView itemView)
        {
            itemView.OnPickup -= ItemViewPickupHandler;
            Model.AddScore(itemView.Score);
            View.Remove(itemView);
            Provider.DisposePickupItem(itemView);
        }
    }
}
