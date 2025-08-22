using JetBrains.Annotations;
using MainCanvas.Configs;
using UnityEngine;
using Zenject;

namespace MainCanvas.Providers
{
    [UsedImplicitly]
    public class MainCanvasProvider : IMainCanvasProvider
    {
        private readonly GameObject _canvas;

        [Inject]
        public MainCanvasProvider([Inject(Id = MainCanvasConstants.MAIN_CANVAS_ID)] GameObject canvas)
        {
            _canvas = canvas;
        }

        public Canvas GetCanvas()
        {
            return _canvas.GetComponent<Canvas>();
        }
    }
}
