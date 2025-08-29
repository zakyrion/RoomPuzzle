using Inventory.Models;
using Inventory.Providers;
using Inventory.UIs;
using JetBrains.Annotations;

namespace Inventory.Presenters
{
    [UsedImplicitly]
    public class InventoryPresenter : IInventoryPresenter
    {
        private readonly IInventoryModel _inventoryModel;
        private readonly IInventoryProvider _inventoryProvider;
        private IInventoryUI _inventoryUI;
        
        public InventoryPresenter(IInventoryModel inventoryModel, IInventoryProvider inventoryProvider)
        {
            _inventoryModel = inventoryModel;
            _inventoryProvider = inventoryProvider;
        }
        
        public void Initialize()
        {
            _inventoryUI = _inventoryProvider.GetInventoryUI();
            _inventoryUI.SetActive(true);
            
            _inventoryModel.OnInventoryChanged += OnInventoryChanged;
            _inventoryModel.OnSlotChanged += OnSlotChanged;
            
            // Initialize UI with current inventory state
            _inventoryUI.UpdateInventory(_inventoryModel.GetAllItems());
        }
        
        public void Dispose()
        {
            if (_inventoryModel != null)
            {
                _inventoryModel.OnInventoryChanged -= OnInventoryChanged;
                _inventoryModel.OnSlotChanged -= OnSlotChanged;
            }
            
            if (_inventoryProvider != null)
            {
                _inventoryProvider.DisposeInventoryUI();
            }
        }
        
        private void OnInventoryChanged(System.Collections.Generic.List<Inventory.Configs.InventoryItem> items)
        {
            _inventoryUI?.UpdateInventory(items);
        }
        
        private void OnSlotChanged(int slotIndex)
        {
            var item = _inventoryModel.GetItem(slotIndex);
            _inventoryUI?.UpdateSlot(slotIndex, item);
        }
    }
}