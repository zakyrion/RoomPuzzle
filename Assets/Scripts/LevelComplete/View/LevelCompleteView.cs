using UnityEngine;
namespace RoomPuzzle.LevelComplete.View
{
    public class LevelCompleteView : MonoBehaviour, ILevelCompleteView
    {
        [SerializeField] private GameObject panel;
        public void Show()
        {
            if (panel != null)
                panel.SetActive(true);
        }
    }
}