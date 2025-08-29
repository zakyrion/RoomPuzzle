using System.Collections.Generic;
using Inventory.Configs;

namespace Inventory.UIs
{
    public interface IInventoryUI
    {
        void SetActive(bool isActive);
        void UpdateInventory(List<InventoryItem> items);
        void UpdateSlot(int slotIndex, InventoryItem item);
    }
}