// METADATA file_path: `Assets/Scripts/PlayerBot/Models/IPlayerBotModel.cs`

using System;
using UnityEngine;

namespace RoomPuzzle.PlayerBot.Models
{
    public interface IPlayerBotModel
    {
        event Action<Vector2> OnMovementInput;
        event Action OnJumpInput;
        event Action OnRunInput;
        
        bool IsEnabled { get; }
        bool IsTargetSeeking { get; }
        Vector3 CurrentTarget { get; }
        
        void SetEnabled(bool enabled);
        void SetTargetSeekingMode(bool enabled);
        void SetCustomTarget(Vector3 target);
        void UpdateBehavior();
        void ResetBehavior();
    }
}