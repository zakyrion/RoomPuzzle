using UnityEngine;    
using Zenject;    
namespace RoomPuzzle    
{    
    public class DoorInstaller : MonoInstaller    
    {    
        [SerializeField] private bool _isLocked = true;    
        [SerializeField] private KeyData _requiredKey;    
        public override void InstallBindings()    
        {    
            Container.Bind<DoorModel>().AsSingle().WithArguments(_isLocked, _requiredKey);    
            Container.BindInterfacesAndSelfTo<DoorPresenter>().AsSingle().NonLazy();    
            Container.Bind<DoorView>().FromComponentInHierarchy().AsSingle();    
        }    
    }    
}