using Inventory.UIs;
using JetBrains.Annotations;
using MainCanvas.Providers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inventory.Providers
{
    [UsedImplicitly]
    public class InventoryProvider : IInventoryProvider
    {
        private const string INVENTORY_UI_PATH = "Prefabs/UI/InventoryUI";
        private readonly IMainCanvasProvider _mainCanvasProvider;
        private GameObject _inventoryGameObject;
        private IInventoryUI _inventoryUI;
        
        public InventoryProvider(IMainCanvasProvider mainCanvasProvider)
        {
            _mainCanvasProvider = mainCanvasProvider;
        }
        
        public IInventoryUI GetInventoryUI()
        {
            if (_inventoryUI != null)
            {
                return _inventoryUI;
            }
            
            _inventoryGameObject = Object.Instantiate(
                Resources.Load<GameObject>(INVENTORY_UI_PATH), 
                _mainCanvasProvider.GetCanvas().transform
            );
            _inventoryUI = _inventoryGameObject.GetComponent<IInventoryUI>();
            return _inventoryUI;
        }
        
        public void DisposeInventoryUI()
        {
            if (_inventoryGameObject != null)
            {
                Object.Destroy(_inventoryGameObject);
                _inventoryGameObject = null;
                _inventoryUI = null;
            }
        }
    }
}