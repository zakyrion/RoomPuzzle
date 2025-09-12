// METADATA file_path: `Assets/Scripts/PlayerBot/UI/IPlayerBotUI.cs`

using System;
using UnityEngine;

namespace RoomPuzzle.PlayerBot.UI
{
    public interface IPlayerBotUI
    {
        event Action OnToggleBotClicked;
        event Action OnResetBehaviorClicked;
        event Action<bool> OnTargetSeekingToggled;
        event Action<Vector3> OnCustomTargetSet;
        
        void UpdateBotStatus(bool isEnabled, bool isTargetSeeking);
        void Show();
        void Hide();
    }
}