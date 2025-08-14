using RoomPuzzle.FinishPoint.View;

namespace RoomPuzzle.FinishPoint.Presenter
{
    /// <summary>
    ///     Responsible for instantiating and unloading the FinishPoint prefab from Resources at runtime.
    /// </summary>
    public interface IFinishPointLoader
    {
        /// <summary>
        ///     Loads the FinishPoint prefab from Resources and returns its View interface.
        /// </summary>
        /// <returns>The instantiated IFinishPointView instance.</returns>
        IFinishPointView Load();

        /// <summary>
        ///     Unloads (destroys) the previously loaded FinishPoint instance.
        /// </summary>
        void Unload();
    }
}
