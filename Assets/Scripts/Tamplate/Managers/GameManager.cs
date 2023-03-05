using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : BaseSingleton<GameManager>
    {
        [SerializeField] LevelManager levelManager;
        [SerializeField] DataBase dataBase;
        [SerializeField] InGameUIController gameUIController;
        [SerializeField] MoneyManager moneyManager;

        [SerializeField] GameInfoSO gameInfoSO;

        #region UNITY_METHODS
        void Start()
        {
            Initialize();
        }
        #endregion

        #region METHODS
        void Initialize()
        {
            if (dataBase != null)
                dataBase.LoadData();
            if (gameInfoSO != null)
            {
                if (moneyManager != null)
                    moneyManager.Initialize();
                if (levelManager != null)
                {
                    levelManager.SetCurrentLevel(gameInfoSO.GetLevelData());
                    levelManager.Initialize();
                }
                if (gameUIController != null)
                    gameUIController.SetLevelText(gameInfoSO.GetLevelData());

            }
        }
        #endregion

        #region ACTIONS
        public void OnNextLevelAction()
        {
            if (gameInfoSO != null)
            {
                gameInfoSO.IncreaseLevelCount();
                if (levelManager != null)
                    levelManager.LoadNextLevel();
                if (moneyManager != null)
                    moneyManager.Initialize();
                if (gameUIController != null)
                    gameUIController.SetLevelText(gameInfoSO.GetLevelData());
            }
            if (dataBase != null)
                dataBase.SaveData();
        }
        public void OnRetryAction()
        {
            if (levelManager != null)
                levelManager.LoadSameLevel();
        }
        #endregion

        [MyBox.ButtonMethod]
        private void ResetData()
        {
            if (dataBase != null)
                dataBase.ResetData();
        }
    }
}