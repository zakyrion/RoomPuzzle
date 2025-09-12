// METADATA file_path: `Assets/Scripts/PlayerBot/Presenters/IPlayerBotPresenter.cs`

using UnityEngine;

namespace RoomPuzzle.PlayerBot.Presenters
{
    public interface IPlayerBotPresenter
    {
        bool IsBotEnabled { get; }
        bool IsTargetSeeking { get; }
        
        void EnableBot(bool enabled);
        void SetTargetSeekingMode(bool enabled);
        void SetCustomTarget(Vector3 target);
        void ToggleBotControl();
        void ResetBotBehavior();
    }
}