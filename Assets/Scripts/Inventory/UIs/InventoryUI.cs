using System.Collections.Generic;
using Inventory.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UIs
{
    public class InventoryUI : MonoBehaviour, IInventoryUI
    {
        [Header("Inventory Slots")]
        [SerializeField] private Image[] _slotImages = new Image[4];
        [SerializeField] private Sprite _emptySlotSprite;
        
        public void SetActive(bool isActive)
        {
            if (!this)
                return;
                
            gameObject.SetActive(isActive);
        }
        
        public void UpdateInventory(List<InventoryItem> items)
        {
            if (!this || items == null)
                return;
                
            for (int i = 0; i < _slotImages.Length; i++)
            {
                UpdateSlot(i, i < items.Count ? items[i] : null);
            }
        }
        
        public void UpdateSlot(int slotIndex, InventoryItem item)
        {
            if (!this || slotIndex < 0 || slotIndex >= _slotImages.Length)
                return;
                
            var slotImage = _slotImages[slotIndex];
            if (slotImage == null)
                return;
                
            if (item == null)
            {
                slotImage.sprite = _emptySlotSprite;
                slotImage.color = Color.white;
            }
            else
            {
                slotImage.sprite = item.Icon != null ? item.Icon : _emptySlotSprite;
                slotImage.color = Color.white;
            }
        }
    }
}