using System.Collections.Generic;
using Game.Utils;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
    public class Upgrade : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] private Text levelText, costText;
        [SerializeField] private UpgradeDataSO data; // ---> declare data this line and change data object name  
        [SerializeField] float clickAnimStr = .2f;
        Tween buttonAnimTween;
        #region UIOperations
        public virtual void UpdateUI()
        {
            levelText.text = "Level " + data.data.valueLevel;
            costText.text = MoneyTextUtility.FloatToStringConverter(data.data.valueUpgradeCost);
        }
        #endregion

        #region UpgradeOperations

        public void IsUpgradeable(float currentMoney)
        {
            if (data.data.valueUpgradeCost <= currentMoney)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        public virtual void DoUpgrade()
        {
            data.UpgradeData();
            UpdateUI();
            ButtonClickAnim();
        }
        private void ButtonClickAnim()
        {
            if (buttonAnimTween != null)
            {
                buttonAnimTween.Rewind();
            }
            buttonAnimTween = transform.DOPunchScale(Vector3.one * clickAnimStr, .2f, 2);
        }
        #endregion
    }
}