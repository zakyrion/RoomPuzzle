using Inventory.Models;
using Inventory.Presenters;
using Inventory.Providers;
using UnityEngine;
using Zenject;

namespace Inventory.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InventoryModel>().AsCached();
            Container.BindInterfacesTo<InventoryProvider>().AsCached();
            Container.BindInterfacesTo<InventoryPresenter>().AsCached().NonLazy();
        }
    }
}