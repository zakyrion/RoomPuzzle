using UnityEngine;
using RoomPuzzle.LevelComplete.View;
namespace RoomPuzzle.LevelComplete.Presenter
{
    public class LevelCompleteLoader : ILevelCompleteLoader
    {
        private const string ResourcePath = "UI/LevelComplete/LevelCompleteUI";
        private GameObject _instance;
        public ILevelCompleteView Load()
        {
            var prefab = Resources.Load<GameObject>(ResourcePath);
            if (prefab == null)
            {
                Debug.LogError($"LevelComplete prefab not found at Resources/{ResourcePath}");
                return null;
            }
            _instance = Object.Instantiate(prefab);
            var view = _instance.GetComponent<ILevelCompleteView>();
            if (view == null)
                Debug.LogError("LevelComplete prefab missing ILevelCompleteView component.");
            return view;
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