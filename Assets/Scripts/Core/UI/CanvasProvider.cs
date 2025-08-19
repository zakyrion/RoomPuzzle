using UnityEngine;

namespace RoomPuzzle.Core.UI
{
    public class CanvasProvider : ICanvasProvider
    {
        private readonly Canvas _canvas;

        public CanvasProvider()
        {
            _canvas = Object.FindObjectOfType<Canvas>();
            if (_canvas == null)
            {
                var canvasGO = new GameObject("MainCanvas");
                _canvas = canvasGO.AddComponent<Canvas>();
                _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGO.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasGO.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }
        }

        public Transform GetCanvasTransform() => _canvas.transform;
    }
}