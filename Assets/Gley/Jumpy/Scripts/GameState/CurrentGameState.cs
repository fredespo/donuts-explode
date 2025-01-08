using Gley.AllPlatformsSave;
using UnityEngine;

namespace Gley.Jumpy
{
    /// <summary>
    /// Keeps track of all game properties
    /// </summary>
    public class GameProgressManager
    {
        //The name of the filename for keeping the save game data
        private const string fileName = "GameValues";
        private SavedProperties savedProperties;

        #region PublicMethods
        /// <summary>
        /// Load data from save
        /// </summary>
        public void LoadGameStatus()
        {
            Gley.AllPlatformsSave.API.Load<SavedProperties>(Application.persistentDataPath + "/" + fileName, LoadDone, false);
        }

        /// <summary>
        /// Save game data
        /// </summary>
        public void SaveGameStatus()
        {
            Gley.AllPlatformsSave.API.Save(savedProperties, Application.persistentDataPath + "/" + fileName, SaveComplete, false);
        }


        /// <summary>
        /// Get player highScore
        /// </summary>
        /// <returns></returns>
        public int GetHighScore()
        {
            return savedProperties.highScore;
        }


        /// <summary>
        /// Set player highScore only if needed
        /// </summary>
        /// <param name="score"></param>
        /// <returns>the current highScore</returns>
        public int SetHighScore(int score)
        {
            if (score > savedProperties.highScore)
            {
                savedProperties.highScore = score;
                SaveGameStatus();
            }
            return savedProperties.highScore;
        }


        /// <summary>
        /// Get the number of lives
        /// </summary>
        /// <returns>number of lives</returns>
        public int GetLives()
        {
            return savedProperties.lives;
        }


        /// <summary>
        /// Decrease the number o lives by 1
        /// </summary>
        internal void DecreaseLives()
        {
            savedProperties.lives--;
            if(savedProperties.lives<0)
            {
                savedProperties.lives = 0;
            }
            SaveGameStatus();
        }


        /// <summary>
        /// Increase the number of lives by 1
        /// </summary>
        public void IncreaseLives()
        {
            savedProperties.lives++;
            SaveGameStatus();
        }

        /// <summary>
        /// Used to set and get volume of special effects
        /// </summary>
        public float FXVolume
        {
            get
            {
                return savedProperties.fxVolume;
            }
            set
            {
                savedProperties.fxVolume = value;
            }
        }


        /// <summary>
        /// Used to set and get background music volume
        /// </summary>
        public float MusicVolume
        {
            get
            {
                return savedProperties.musicVolume;
            }
            set
            {
                savedProperties.musicVolume = value;
            }
        }
        #endregion


        #region PrivateMethods
        /// <summary>
        /// Load data callback
        /// </summary>
        /// <param name="data">the actual data</param>
        /// <param name="result">the result of the load process: Success/Error/Empty</param>
        /// <param name="message"></param>
        private void LoadDone(SavedProperties data, SaveResult result, string message)
        {
            if (result == SaveResult.Success)
            {
                savedProperties = data;
            }
            else
            {
                savedProperties = new SavedProperties();
                Debug.Log("Load failed " + message);
            }
        }


        /// <summary>
        /// Save completed callback
        /// </summary>
        /// <param name="result"></param>
        /// <param name="error"></param>
        private void SaveComplete(SaveResult result, string error)
        {

        }
        #endregion
    }
}
