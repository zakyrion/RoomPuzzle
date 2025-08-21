using System;
using Player.Configs;
using UnityEngine;

namespace Player.Models
{
    public interface IPlayerModel
    {
        event Action<Vector3> OnPlayerPositionChanged;
        event Action OnPlayerSpawned;

        float Speed { get; }
        float JumpForce { get; }

        IPlayerConfig GetConfig();
        void PlayerSpawned();
        void SetPosition(Vector3 position);
    }
}
