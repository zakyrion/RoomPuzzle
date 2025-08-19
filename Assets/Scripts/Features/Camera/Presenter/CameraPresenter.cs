// METADATA file_path: Assets/Scripts/Features/Camera/Presenter/CameraPresenter.cs
using RoomPuzzle.Features.Camera.View;
using RoomPuzzle.Features.Player.Model;
using Zenject;
public class CameraPresenter : IInitializable, ITickable
{
    private readonly ICameraView _cameraView;
    private readonly IPlayerModel _playerModel;
    public CameraPresenter(ICameraView cameraView, IPlayerModel playerModel)
    {
        _cameraView = cameraView;
        _playerModel = playerModel;
    }
    public void Initialize() { }
    public void Tick()
    {
        if (_playerModel != null)
            _cameraView.Follow(_playerModel.Position);
    }
}
