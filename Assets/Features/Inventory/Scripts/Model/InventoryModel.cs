using System.Collections.Generic;

namespace RoomPuzzle
{
    public class InventoryModel
    {
        private readonly List<KeyData> _keys = new();

        public bool AddKey(KeyData key)
        {
            if (key == null)
                return false;
            _keys.Add(key);
            return true;
        }

        public bool HasKey(KeyData key)
        {
            return key != null && _keys.Contains(key);
        }

        public void UseKey(KeyData key)
        {
            if (HasKey(key))
                _keys.Remove(key);
        }
    }
}
