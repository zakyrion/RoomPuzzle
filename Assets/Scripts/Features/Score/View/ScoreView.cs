using TMPro;
using UnityEngine;

// Make sure TextMeshPro is imported
namespace RoomPuzzle.Features.Score.View
{
    public class ScoreView : MonoBehaviour, IScoreView
    {
        [SerializeField] private TMP_Text _scoreText;

        private void Awake()
        {
            if (_scoreText == null)
            {
                _scoreText = GetComponent<TMP_Text>();
            }
        }

        public void UpdateScoreText(string text)
        {
            if (_scoreText != null)
            {
                _scoreText.text = text;
            }
        }
    }
}
