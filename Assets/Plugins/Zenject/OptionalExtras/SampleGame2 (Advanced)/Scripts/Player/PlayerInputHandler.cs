using UnityEngine;
using System;
namespace RoomPuzzle
{
    /// <summary>
    /// VIEW (Component): Its only responsibility is to detect raw input
    /// and fire an event. It has no knowledge of what interaction means.
    /// </summary>
    public class PlayerInputHandler : MonoBehaviour
    {
        public event Action OnInteractPressed;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteractPressed?.Invoke();
            }
        }
    }
}
