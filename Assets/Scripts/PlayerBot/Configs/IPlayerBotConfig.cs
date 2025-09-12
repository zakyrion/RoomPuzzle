// METADATA file_path: `Assets/Scripts/PlayerBot/Configs/IPlayerBotConfig.cs`

namespace RoomPuzzle.PlayerBot.Configs
{
    public interface IPlayerBotConfig
    {
        float ActionInterval { get; }
        float DirectionChangeInterval { get; }
        float RunProbability { get; }
        float JumpProbability { get; }
        float SearchRadius { get; }
        float TargetReachDistance { get; }
        float StuckThreshold { get; }
        float StuckCheckInterval { get; }
        float StuckRecoveryTime { get; }
        bool EnableTargetSeeking { get; }
        bool EnableRandomMovement { get; }
    }
}