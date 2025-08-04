using UnityEngine;    
using Zenject;    
namespace RoomPuzzle    
{    
    public class GameSceneInstaller : MonoInstaller    
    {    
        public override void InstallBindings()    
        {    
            //Container.InstantiatePrefabResource("Prefabs/Player", Vector3.zero, Quaternion.identity, null);
        }    
    }    
}