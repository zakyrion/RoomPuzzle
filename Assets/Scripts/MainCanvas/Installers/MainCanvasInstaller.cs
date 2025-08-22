using MainCanvas.Configs;
using MainCanvas.Providers;
using UnityEngine;
using Zenject;

namespace MainCanvas.Installers
{
    public class MainCanvasInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _canvas;

        public override void InstallBindings()
        {
            Container.Bind<GameObject>().WithId(MainCanvasConstants.MAIN_CANVAS_ID).FromInstance(_canvas).AsCached();
            Container.BindInterfacesAndSelfTo<MainCanvasProvider>().AsCached();
        }
    }
}