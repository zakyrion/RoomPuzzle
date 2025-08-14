using RoomPuzzle.Level;

namespace RoomPuzzle.FinishPoint.Model
{
    public class FinishPointModel : IFinishPointModel
    {
        private readonly ILevelManager _levelManager;

        public FinishPointModel(ILevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public void HandlePlayerReached()
        {
            _levelManager.CompleteLevel();
        }
    }
}
