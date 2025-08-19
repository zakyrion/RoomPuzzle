using UnityEngine;

namespace RoomPuzzle.Features.Player.View
{
    public interface IPlayerView
    {
        Transform Transform { get; }
        void SetMoveDirection(Vector3 direction);
        void Jump(float force);

        // Existing event(s), keep them if present
        event System.Action<RoomPuzzle.Features.Pickups.View.PickupView> OnPickupCollected;
        bool IsGrounded { get; }
    }
}