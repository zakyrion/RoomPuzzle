using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Configs;
using JetBrains.Annotations;

namespace Inventory.Models
{
    [UsedImplicitly]
    public class InventoryModel : IInventoryModel
    {
        public event Action<List<InventoryItem>> OnInventoryChanged;
        public event Action<int> OnSlotChanged;
        
        private const int MAX_SLOTS = 4;
        private readonly List<InventoryItem> _items;
        
        public InventoryModel()
        {
            _items = new List<InventoryItem>(MAX_SLOTS);
            // Initialize with empty slots
            for (int i = 0; i < MAX_SLOTS; i++)
            {
                _items.Add(null);
            }
        }
        
        public bool HasFreeSlot()
        {
            return _items.Any(item => item == null);
        }
        
        public bool AddItem(InventoryItem item)
        {
            if (item == null || !HasFreeSlot())
                return false;
                
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;
                    OnSlotChanged?.Invoke(i);
                    OnInventoryChanged?.Invoke(GetAllItems());
                    return true;
                }
            }
            
            return false;
        }
        
        public bool RemoveItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= MAX_SLOTS || _items[slotIndex] == null)
                return false;
                
            _items[slotIndex] = null;
            OnSlotChanged?.Invoke(slotIndex);
            OnInventoryChanged?.Invoke(GetAllItems());
            return true;
        }
        
        public InventoryItem GetItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= MAX_SLOTS)
                return null;
                
            return _items[slotIndex];
        }
        
        public List<InventoryItem> GetAllItems()
        {
            return new List<InventoryItem>(_items);
        }
        
        public int GetMaxSlots()
        {
            return MAX_SLOTS;
        }
        
        public int GetUsedSlots()
        {
            return _items.Count(item => item != null);
        }
        
        public void ClearInventory()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i] = null;
            }
            OnInventoryChanged?.Invoke(GetAllItems());
        }
    }
}