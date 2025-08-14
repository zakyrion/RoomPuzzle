using System;
using RoomPuzzle.Features.Pickups.View;
using UnityEngine;

namespace RoomPuzzle.Features.Player.View
{
    public interface IPlayerView
    {
        event Action<PickupView> OnPickupCollected;
        void Jump(float force);
        void SetMoveDirection(Vector3 direction);
    }
}
