using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MoneyManager : BaseSingleton<MoneyManager>
    {
        [SerializeField] GameInfoSO gameInfoSO;
        #region UNITY_METHODS
        void OnEnable()
        {
            //EventManager.StartListening(Events.CHANGE_MONEY, onMoneyChangeEvent);
            EventManager.StartListening(Events.ADD_MONEY, OnMoneyAddEvent);
        }
        void OnDisable()
        {
            //EventManager.StopListening(Events.CHANGE_MONEY, onMoneyChangeEvent);
            EventManager.StopListening(Events.ADD_MONEY, OnMoneyAddEvent);
        }
        #endregion

        #region METHODS
        #endregion
        #region ACTIONS
        //private void onMoneyChangeEvent(object[] obj)
        //{

        //}
        public void Initialize()
        {
            float value = gameInfoSO.GetPlayerMoney();
            EventManager.TriggerEvent(Events.UPDATE_TOTAL_MONEY_UI, true, new object[] { value });
        }
        private void OnMoneyAddEvent(object[] obj)
        {
            int money = (int)obj[0];
            gameInfoSO.SumPlayerMoney(money);
            EventManager.TriggerEvent(Events.UPDATE_FINISH_MONEY_UI, true, new object[] {money});
            DataBase.instance.SaveData();
        }
        #endregion
    }
}