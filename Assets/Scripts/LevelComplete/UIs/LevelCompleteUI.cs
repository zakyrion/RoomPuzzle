using UnityEngine;

namespace RoomPuzzle.LevelComplete.View
{
    public class LevelCompleteUI : MonoBehaviour, ILevelCompleteUI
    {
        [SerializeField] private GameObject panel;

        private void Awake()
        {
            if (panel != null)
                panel.SetActive(false); // hide at start
        }

        public void Show()
        {
            if (panel != null) panel.SetActive(true);
        }

        public void Hide()
        {
            if (panel != null) panel.SetActive(false);
        }
    }
}