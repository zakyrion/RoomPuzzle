using RoomPuzzle.LevelComplete.View;
namespace RoomPuzzle.LevelComplete.Presenter
{
    public interface ILevelCompleteLoader
    {
        ILevelCompleteUI Load();
        void Unload();
    }
}