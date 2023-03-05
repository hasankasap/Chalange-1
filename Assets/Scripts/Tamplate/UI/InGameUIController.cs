using DG.Tweening;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class InGameUIController : MonoBehaviour
    {
        [SerializeField] GameObject levelFailUI, levelCompleteUI;
        [SerializeField] Text levelCountText;
        [SerializeField] UITutorial uiTutorial;
        //[SerializeField] ProgressBar progressBar;

        #region MONEY
        [SerializeField, MyBox.Foldout("Money Things")] Text finishMoneyText, totalMoneyText;
        [SerializeField, MyBox.Foldout("Money Things")] Transform moneyAnimImageSpawnPos, moneyAnimTarget;
        [SerializeField, MyBox.Foldout("Money Things")] Image moneyAnimTargetImage;
        [SerializeField, MyBox.Foldout("Money Things")] int maxImageCount;
        [SerializeField, MyBox.Foldout("Money Things")] float spawnRange;
        int finishMoney;
        float totalMoney;
        #endregion


        #region UNITY_METHODS
        void OnEnable()
        {
            EventManager.StartListening(Events.LEVEL_FAILED, OnLevelFailed);
            EventManager.StartListening(Events.LEVEL_COMPLETED, OnLevelComplete);
            EventManager.StartListening(Events.LEVEL_LOADED, OnLevelLoadded);
            EventManager.StartListening(Events.UPDATE_FINISH_MONEY_UI, OnFinishMoneyUpdate);
            EventManager.StartListening(Events.UPDATE_TOTAL_MONEY_UI, OnTotalMoneyUpdate);
        }
        void OnDisable()
        {
            EventManager.StopListening(Events.LEVEL_FAILED, OnLevelFailed);
            EventManager.StopListening(Events.LEVEL_COMPLETED, OnLevelComplete);
            EventManager.StopListening(Events.LEVEL_LOADED, OnLevelLoadded);
            EventManager.StopListening(Events.UPDATE_FINISH_MONEY_UI, OnFinishMoneyUpdate);
            EventManager.StopListening(Events.UPDATE_TOTAL_MONEY_UI, OnTotalMoneyUpdate);
        }
        #endregion

        #region METHODS
        public void Initialize()
        {
            RefreshUI();
        }
        private void RefreshUI()
        {
            levelFailUI.SetActive(false);
            levelCompleteUI.SetActive(false);
            levelCountText.gameObject.SetActive(true);
        }
        private void CloseAllUI()
        {
            levelFailUI.SetActive(false);
            levelCompleteUI.SetActive(false);
            levelCountText.gameObject.SetActive(false);
        }
        private void LevelFailActions()
        {
            levelFailUI.SetActive(true);
            levelCompleteUI.SetActive(false);
            levelCountText.gameObject.SetActive(false);
        }
        private void LevelCompleteActions()
        {
            levelFailUI.SetActive(false);
            levelCompleteUI.SetActive(true);
            MoneyUIAnim(finishMoney, .05f);
            levelCountText.gameObject.SetActive(false);
        }
        public void MoneyUIAnim(float value, float minDelay)
        {
            int count = Mathf.FloorToInt(value);
            if (count > maxImageCount)
                count = maxImageCount;
            float randomX, randomY;
            Vector3 spawnPos;
            if (count < 3)
                count = 3;
            for (int i = 0; i < count; i++)
            {
                randomX = Random.Range(-spawnRange / 2, spawnRange / 2);
                randomY = Random.Range(-spawnRange / 2, spawnRange / 2);
                spawnPos = moneyAnimImageSpawnPos.position + new Vector3(randomX, randomY, 0);
                GameObject temp = Instantiate(moneyAnimTargetImage.gameObject, moneyAnimImageSpawnPos.position, moneyAnimTargetImage.transform.rotation, transform);
                Transform image = temp.transform;
                float delay = minDelay + (i * .01f);
                image.DOMove(spawnPos, .2f).OnComplete(() => image.DOMove(moneyAnimTarget.position, .5f).SetDelay(delay).OnComplete(() => Destroy(temp)));
            }
            totalMoneyText.text = MoneyTextUtility.FloatToStringConverter(totalMoney + finishMoney);
        }
        public void SetLevelText(int levelIndex)
        {
            levelCountText.text = "Level " + (levelIndex + 1).ToString();
        }
        #endregion

        #region ACTIONS
        private void OnFinishMoneyUpdate(object[] obj)
        {
            finishMoney = (int)obj[0];
            finishMoneyText.text = MoneyTextUtility.FloatToStringConverter(finishMoney);
        }
        private void OnTotalMoneyUpdate(object[] obj)
        {
            float value = (float)obj[0];
            totalMoney = value;
            totalMoneyText.text = MoneyTextUtility.FloatToStringConverter(totalMoney);
        }
        private void OnLevelFailed(object[] obj)
        {
            LevelFailActions();
        } 
        private void OnLevelComplete(object[] obj)
        {
            LevelCompleteActions();
        }
        private void OnLevelLoadded(object[] obj)
        {
            RefreshUI();
            uiTutorial.ResetTutorial();
        }
        public void OnNextButtonClicked()
        {
            CloseAllUI();
            GameManager.instance.OnNextLevelAction();
        }        
        public void OnRetryButtonClicked()
        {
            CloseAllUI();
            GameManager.instance.OnRetryAction();
        }
        #endregion
    }
}