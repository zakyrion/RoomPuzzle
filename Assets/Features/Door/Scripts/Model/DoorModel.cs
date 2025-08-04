using System;

namespace RoomPuzzle
{
    public class DoorModel
    {
        public event Action OnStateChanged;
        public bool IsLocked { get; private set; }
        public bool IsOpen { get; private set; }
        public KeyData RequiredKey { get; }

        public DoorModel(bool isLocked, KeyData requiredKey)
        {
            IsLocked = isLocked;
            RequiredKey = requiredKey;
        }

        /// <summary>
        ///     Contains the core logic for opening the door.
        /// </summary>
        /// <returns>True if the door was successfully opened.</returns>
        public bool AttemptToOpen(InventoryModel inventory)
        {
            if (IsOpen)
                return false; // Already open, no state change.
            var canOpen = !IsLocked || RequiredKey != null && inventory.HasKey(RequiredKey);
            if (canOpen)
            {
                if (IsLocked)
                {
                    inventory.UseKey(RequiredKey);
                }
                IsLocked = false;
                IsOpen = true;
                OnStateChanged?.Invoke(); // Notify listeners (the presenter) of the change.    
                return true;
            }
            return false;
        }
    }
}
