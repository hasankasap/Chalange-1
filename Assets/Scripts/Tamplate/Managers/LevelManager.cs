using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LevelManager : BaseSingleton<LevelGenerator>
    {
        [SerializeField] private LevelSO[] levelPrefabs;
        private Level currentLevelPrefab;
        private int currentLevel = 0;
        [SerializeField] LevelGenerator levelGenerator;
        [SerializeField] int startIndex = 0;

        #region UNITY_METHODS
        #endregion

        #region METHODS
        public void Initialize()
        {
            LoadLevel(currentLevel);
        }
        
        public void SetCurrentLevel(int currentIndex)
        {
            currentLevel = currentIndex;
        }
      
        private void LoadLevel(int index)
        {
            if (levelPrefabs.Length == 0)
            {
                Debug.Log("there is no attached level");
                return;
            }
            if (index < 0)
            {
                Debug.LogError("Invalid level index: " + index);
                return;
            }
            if (index >= levelPrefabs.Length)
            {
                index %= levelPrefabs.Length;
                if (index < startIndex)
                    index = startIndex;
            }
                // Destroy any existing level
            if (currentLevelPrefab != null)
            {
                Destroy(currentLevelPrefab.gameObject);
            }

            // Instantiate the new level
            currentLevelPrefab = levelGenerator.generateLevel(levelPrefabs[index].GetLevelPrefab());
            EventManager.TriggerEvent(Events.LEVEL_LOADED, false);
        }
        #endregion

        #region ACTIONS
        public void LoadSameLevel()
        {
            LoadLevel(currentLevel);
        }
        public void LoadNextLevel()
        {
            currentLevel++;
            LoadLevel(currentLevel);
        }
        #endregion
    }
}