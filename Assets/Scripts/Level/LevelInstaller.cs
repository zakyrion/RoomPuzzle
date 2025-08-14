using Zenject;
namespace RoomPuzzle.Level
{
    /// <summary>
    /// Zenject installer for Level domain logic.
    /// </summary>
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILevelManager>().To<LevelManager>().AsSingle();
        }
    }
}