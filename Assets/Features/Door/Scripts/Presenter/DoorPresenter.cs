using System;
using UnityEngine;
using Zenject;

namespace RoomPuzzle
{
    public class DoorPresenter : IInitializable, IDisposable
    {
        private readonly InventoryModel _inventory;
        private readonly DoorModel _model;
        private readonly DoorView _view;

        public DoorPresenter(DoorView view, DoorModel model, InventoryModel inventory)
        {
            _view = view;
            _model = model;
            _inventory = inventory;
        }

        public void Dispose()
        {
            _view.OnTryOpen -= HandleTryOpen;
            _model.OnStateChanged -= HandleStateChanged;
        }

        public void Initialize()
        {
            _view.OnTryOpen += HandleTryOpen;
            _model.OnStateChanged += HandleStateChanged;
            // Set initial visual state    
            _view.UpdateVisuals(_model.IsOpen);
        }

        // 3. Model signals that its state has changed.    
        private void HandleStateChanged()
        {
            // 4. Presenter updates the view based on the new model state.    
            _view.UpdateVisuals(_model.IsOpen);
            Debug.Log("Door opened.");
        }

        // 1. View signals an intent to open.    
        private void HandleTryOpen()
        {
            // 2. Presenter asks the model to perform the logic.    
            var opened = _model.AttemptToOpen(_inventory);
            // Presenter can provide immediate feedback if logic fails.    
            if (!opened && _model.IsLocked)
            {
                Debug.Log("Door is locked. You don't have the right key.");
            }
        }
    }
}
