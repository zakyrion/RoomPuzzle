using PlayerMovement.Configs;
using UnityEngine;

namespace PlayerMovement.Views
{
    public interface IPlayerMovementView
    {
        Vector3 Position { get; }
        bool Grounded { get; }
        void Idle();
        void Initialize(IPlayerMovementConfig playerMovementConfig);
        void Jump(float jumpForce);
        void Move(Vector2 move, float speed);
        void SetRotation(float rotation);
    }
}
