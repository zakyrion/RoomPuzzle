// METADATA file_path: Assets/Scripts/Features/Player/Presenter/PlayerPresenter.cs    
using System;
using RoomPuzzle.Features.Input.Model;
using RoomPuzzle.Features.Pickups.View;
using RoomPuzzle.Features.Player.Loader;
using RoomPuzzle.Features.Player.Model;
using RoomPuzzle.Features.Player.Presenter;
using RoomPuzzle.Features.Player.View;
using RoomPuzzle.Features.Score.Model;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class PlayerPresenter : IPlayerPresenter, IInitializable, ITickable, IDisposable
{
    private readonly IInputModel _inputModel;
    private readonly IPlayerLoader _playerLoader;
    private readonly IPlayerModel _playerModel;
    private readonly IScoreModel _scoreModel;
    private IPlayerView _playerView;

    public PlayerPresenter(
        IInputModel inputModel,
        IPlayerModel playerModel,
        IScoreModel scoreModel,
        IPlayerLoader playerLoader)
    {
        _inputModel = inputModel;
        _playerModel = playerModel;
        _scoreModel = scoreModel;
        _playerLoader = playerLoader;
    }

    public void Dispose()
    {
        if (_playerView != null)
            _playerView.OnPickupCollected -= HandlePickupCollected;
        _inputModel.OnJumpPressed -= HandleJump;
    }

    public void Initialize()
    {
        _playerView = _playerLoader.Create(Vector3.zero);
        _playerView.OnPickupCollected += HandlePickupCollected;
        _inputModel.OnJumpPressed += HandleJump;
    }

    public void Tick()
    {
        var moveDir = new Vector3(_inputModel.Horizontal, 0, _inputModel.Vertical);
        if (moveDir.sqrMagnitude > 0.01f)
            _playerView.SetMoveDirection(moveDir * _playerModel.MoveSpeed);
        // 🔑 Update PlayerModel with current player position
        if (_playerView != null)
        {
            _playerModel.IsGrounded = _playerView.IsGrounded;
            _playerModel.Position = _playerView.Transform.position;
        }

    }

    private void HandleJump()
    {
        if (_playerModel.IsGrounded)
            _playerView.Jump(_playerModel.JumpForce);
    }

    private void HandlePickupCollected(PickupView pickup)
    {
        _scoreModel.AddScore(1);
        Object.Destroy(pickup.gameObject);
    }
}
