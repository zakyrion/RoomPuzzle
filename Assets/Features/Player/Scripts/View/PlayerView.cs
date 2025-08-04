using UnityEngine;
namespace RoomPuzzle
{
    /// <summary>
    /// VIEW (Facade): Acts as a single entry point to all player components.
    /// It doesn't contain logic itself but holds references to specialized components.
    /// This follows the Facade pattern and respects the Single Responsibility Principle.
    /// </summary>
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerLook))]
    [RequireComponent(typeof(PlayerInteraction))]
    public class PlayerView : MonoBehaviour
    {
        // References to specialized components, populated automatically
        public PlayerMovement Movement { get; private set; }
        public PlayerLook Look { get; private set; }
        public PlayerInteraction Interaction { get; private set; }
        private void Awake()
        {
            // Populate references at runtime
            Movement = GetComponent<PlayerMovement>();
            Look = GetComponent<PlayerLook>();
            Interaction = GetComponent<PlayerInteraction>();
        }
    }
}
