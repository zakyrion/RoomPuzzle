using System;
using PlayerScore.Configs;
using UnityEngine;

namespace Inventory.Configs
{
    [Serializable]
    public class InventoryItem
    {
        [SerializeField]
        public string Name;
        
        [SerializeField]
        public PlayerScorePickupItemType Type;
        
        [SerializeField]
        public int Score;
        
        [SerializeField]
        public Sprite Icon;
        
        public InventoryItem(string name, PlayerScorePickupItemType type, int score, Sprite icon = null)
        {
            Name = name;
            Type = type;
            Score = score;
            Icon = icon;
        }
        
        public InventoryItem()
        {
            Name = string.Empty;
            Type = PlayerScorePickupItemType.Unknown;
            Score = 0;
            Icon = null;
        }
    }
}