using TMPro;
using UnityEngine;

namespace PlayerScore.UIs
{
    public class PlayerScoreUI : MonoBehaviour, IPlayerScoreUI
    {
        [SerializeField]
        private TMP_Text _scoreText;

        public void SetActive(bool isActive)
        {
            if (!this)
            {
                return;
            }

            gameObject.SetActive(isActive);
        }

        public void SetScore(int score)
        {
            if (!this)
            {
                return;
            }

            _scoreText.text = score.ToString();
        }
    }
}
