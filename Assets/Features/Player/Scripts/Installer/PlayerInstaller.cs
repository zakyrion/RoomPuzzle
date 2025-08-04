using Zenject;

namespace RoomPuzzle
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Binds components from the player prefab's hierarchy to the container
            Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerLook>().FromComponentInHierarchy().AsSingle(); // <-- ADDED THIS LINE
            Container.Bind<PlayerInteraction>().FromComponentInHierarchy().AsSingle();
        }
    }
}
