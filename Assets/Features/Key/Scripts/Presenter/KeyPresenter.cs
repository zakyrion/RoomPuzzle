using Zenject;    
using System;    
namespace RoomPuzzle    
{    
    public class KeyPresenter : IInitializable, IDisposable    
    {    
        private readonly KeyView _view;    
        private readonly InventoryModel _inventory;    
        public KeyPresenter(KeyView view, InventoryModel inventory) { _view = view; _inventory = inventory; }    
        public void Initialize() => _view.OnPickedUp += HandlePickup;    
        public void Dispose() => _view.OnPickedUp -= HandlePickup;    
        private void HandlePickup(KeyView keyView)    
        {    
            if (_inventory.AddKey(keyView.KeyData)) { keyView.DestroyView(); }    
        }    
    }    
}