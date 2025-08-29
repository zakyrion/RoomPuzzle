using System;
using System.Collections.Generic;
using Inventory.Configs;

namespace Inventory.Models
{
    public interface IInventoryModel
    {
        event Action<List<InventoryItem>> OnInventoryChanged;
        event Action<int> OnSlotChanged;
        
        bool HasFreeSlot();
        bool AddItem(InventoryItem item);
        bool RemoveItem(int slotIndex);
        InventoryItem GetItem(int slotIndex);
        List<InventoryItem> GetAllItems();
        int GetMaxSlots();
        int GetUsedSlots();
        void ClearInventory();
    }
}