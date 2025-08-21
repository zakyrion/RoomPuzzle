using Player.Configs;
using UnityEngine;

namespace Player.Views
{
    public interface IPlayerView
    {
        Vector3 Position { get; }
        void Initialize(IPlayerConfig playerConfig);
        void SetRotation(float rotation);
        void Jump(float jumpForce);
        void Move(Vector2 move, float speed);
    }
}
