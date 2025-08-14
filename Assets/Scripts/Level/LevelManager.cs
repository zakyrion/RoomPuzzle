using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace RoomPuzzle.Level
{
    public class LevelManager : ILevelManager
    {
        public event Action LevelFinished;
        public void CompleteLevel()
        {
            LevelFinished?.Invoke();
            Debug.Log("Level Complete!");
            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.Log("No more levels available. Returning to main menu or ending game.");
                // Optionally load main menu:
                // SceneManager.LoadScene(0);
            }
        }

        public void RestartLevel()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}