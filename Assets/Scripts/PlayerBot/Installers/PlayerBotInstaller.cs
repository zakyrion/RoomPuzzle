// METADATA file_path: `Assets/Scripts/PlayerBot/Installers/PlayerBotInstaller.cs`

using RoomPuzzle.PlayerBot.Configs;
using RoomPuzzle.PlayerBot.Models;
using RoomPuzzle.PlayerBot.Presenters;
using RoomPuzzle.PlayerBot.UI;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.PlayerBot.Installers
{
    public class PlayerBotInstaller : MonoInstaller
    {
        [SerializeField] private PlayerBotConfig _playerBotConfig;
        [SerializeField] private PlayerBotUI _playerBotUIPrefab;
        
        public override void InstallBindings()
        {
            BindConfig();
            BindModel();
            BindPresenter();
            //BindUI();
        }
        
        private void BindConfig()
        {
            Container
                .Bind<IPlayerBotConfig>()
                .FromInstance(_playerBotConfig)
                .AsCached()
                .NonLazy();
        }
        
        private void BindModel()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerBotModel>()
                .AsCached()
                .NonLazy();
        }
        
        private void BindPresenter()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerBotPresenter>()
                .AsCached()
                .NonLazy();
        }
        
        private void BindUI()
        {
            if (_playerBotUIPrefab != null)
            {
                Container
                    .Bind<IPlayerBotUI>()
                    .FromComponentInNewPrefab(_playerBotUIPrefab)
                    .AsCached()
                    .NonLazy();
                    
                Container
                    .BindInterfacesTo<PlayerBotUIPresenter>()
                    .AsCached()
                    .NonLazy();
            }
        }
    }
}