using RoomPuzzle.LevelComplete.View;
namespace RoomPuzzle.LevelComplete.Presenter
{
    public interface ILevelCompleteLoader
    {
        ILevelCompleteView Load();
        void Unload();
    }
}