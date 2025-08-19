using RoomPuzzle.Features.Player.View;
using UnityEngine;

namespace RoomPuzzle.Features.Player.Loader
{
    public interface IPlayerLoader
    {
        IPlayerView Create(Vector3 position);
    }
}
