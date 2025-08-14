using System;
namespace RoomPuzzle.Level
{
    public interface ILevelManager
    {
        event Action LevelFinished;
        /// <summary>    
        /// Called when level completion conditions are met.    
        /// </summary>    
        void CompleteLevel();
        /// <summary>    
        /// Reloads the current level.    
        /// </summary>    
        void RestartLevel();
        /// <summary>    
        /// Loads the next level if available.    
        /// </summary>    
        void LoadNextLevel();
    }
}