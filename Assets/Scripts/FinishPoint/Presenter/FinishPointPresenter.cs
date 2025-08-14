using RoomPuzzle.FinishPoint.Model;
using RoomPuzzle.FinishPoint.View;
namespace RoomPuzzle.FinishPoint.Presenter
{
    public class FinishPointPresenter : IFinishPointPresenter
    {
        private readonly IFinishPointModel _model;
        private readonly IFinishPointView _view;
        private readonly IFinishPointLoader _loader;
        public FinishPointPresenter(
            IFinishPointModel model,
            IFinishPointView view,
            IFinishPointLoader loader)
        {
            _model = model;
            _view = view;
            _loader = loader;
        }
        public void Initialize()
        {
            _view.PlayerReached += OnPlayerReached;
        }
        public void Dispose()
        {
            _view.PlayerReached -= OnPlayerReached;
        }
        private void OnPlayerReached()
        {
            _model.HandlePlayerReached();
            _loader.Unload();
        }
    }
}