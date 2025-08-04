using Zenject;    
using UnityEngine;    
namespace RoomPuzzle    
{    
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]    
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>    
    {    
        public override void InstallBindings()    
        {    
            Container.BindInterfacesAndSelfTo<InventoryModel>().AsSingle();    
        }    
    }    
}