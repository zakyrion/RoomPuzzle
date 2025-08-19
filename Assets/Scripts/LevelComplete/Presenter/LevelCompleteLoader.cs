using UnityEngine;
using RoomPuzzle.LevelComplete.View;
using RoomPuzzle.Core.UI;

namespace RoomPuzzle.LevelComplete.Presenter
{
    public class LevelCompleteLoader : ILevelCompleteLoader
    {
        private const string ResourcePath = "UI/LevelComplete/LevelCompleteUI";
        private GameObject _instance;
        private readonly ICanvasProvider _canvasProvider;

        public LevelCompleteLoader(ICanvasProvider canvasProvider)
        {
            _canvasProvider = canvasProvider;
        }

        public ILevelCompleteUI Load()
        {
            var prefab = Resources.Load<GameObject>(ResourcePath);
            if (prefab == null)
            {
                Debug.LogError($"LevelCompleteUI prefab not found at Resources/{ResourcePath}");
                return null;
            }

            _instance = Object.Instantiate(prefab, _canvasProvider.GetCanvasTransform(), false);
            var ui = _instance.GetComponent<ILevelCompleteUI>();
            if (ui == null)
            {
                Debug.LogError("LevelCompleteUI prefab missing ILevelCompleteUI component!");
                return null;
            }

            return ui;
        }

        public void Unload()
        {
            if (_instance != null)
            {
                Object.Destroy(_instance);
                _instance = null;
            }
        }
    }
}