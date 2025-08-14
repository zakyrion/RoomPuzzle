using RoomPuzzle.FinishPoint.View;
using UnityEngine;

namespace RoomPuzzle.FinishPoint.Presenter
{
    /// <summary>
    ///     Instantiates and destroys the FinishPoint prefab from Resources at runtime.
    /// </summary>
    public class FinishPointLoader : IFinishPointLoader
    {
        // Path relative to a Resources folder.
        private const string ResourcePath = "Prefabs/FinishPoint/FinishPoint";
        private GameObject _instance;

        /// <summary>
        ///     Loads the FinishPoint prefab and returns its View component.
        /// </summary>
        public IFinishPointView Load()
        {
            var prefab = Resources.Load<GameObject>(ResourcePath);
            if (prefab == null)
            {
                Debug.LogError($"FinishPoint prefab not found at Resources/{ResourcePath}");
                return null;
            }
            _instance = Object.Instantiate(prefab);
            var view = _instance.GetComponent<IFinishPointView>();
            if (view == null)
            {
                Debug.LogError("FinishPoint prefab does not contain a component implementing IFinishPointView.");
            }
            return view;
        }

        /// <summary>
        ///     Destroys the loaded FinishPoint instance.
        /// </summary>
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
