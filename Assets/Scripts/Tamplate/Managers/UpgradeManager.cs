using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
    public class UpgradeManager : MonoBehaviour 
    {
        [SerializeField] GameInfoSO gameInfoSO;
        [SerializeField] GameObject upgradeUI;
        [SerializeField] Upgrade[] upgrades;
        #region UNITY METHODS
        void Start()
        {
            UpdateAllUI();
            CheckUpgradeables();
        }
        private void OnEnable()
        {
            //LEVEL_LOADED add event listener
            //GAME_STARTED add event listener
        }
        private void OnDisable()
        {
            //LEVEL_LOADED remove event listener
            //GAME_STARTED event listener
        }
        #endregion

        #region METHOD
        private void UpdateAllUI()
        {
            foreach (Upgrade upgrade in upgrades)
            {
                upgrade.UpdateUI();
            }
        }
        public void CheckUpgradeables()
        {
            foreach (Upgrade upgrade in upgrades)
            {
                upgrade.IsUpgradeable(gameInfoSO.GetPlayerMoney());
            }
        }
        #endregion

        #region ACTION
        private void OpenUpgradeUI(object[] obj)
        {
            upgradeUI.SetActive(true);
            UpdateAllUI();
            CheckUpgradeables();
        }
        public void CloseUpgradeUI(object[] obj)
        {
            upgradeUI.SetActive(false);
        }
        #endregion

    }
}