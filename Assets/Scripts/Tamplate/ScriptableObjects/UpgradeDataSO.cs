using Game.Utils;
using UnityEngine;

namespace Game
{

    [CreateAssetMenu(fileName = "UpgradeDataSo", menuName = "ScriptableObjects/UpgradeDataSO")]
    public class UpgradeDataSO : DataSO
    {
        [SerializeField] UpgradeDataSO defaultSO;
        public UpgradeInfoData data = new UpgradeInfoData();
        public override void ResetData()
        {
            base.ResetData();
            if (defaultSO != null)
            {
                data.value = defaultSO.data.value;
                data.valueIncrease = defaultSO.data.valueIncrease;
                data.valueLevel = defaultSO.data.valueLevel;
                data.valueUpgradeCost = defaultSO.data.valueUpgradeCost;
                data.valueNumberCostIncrease = defaultSO.data.valueNumberCostIncrease;
            }
        }
        public override void SaveData()
        {
            base.SaveData();
            DataBase.instance.Save(data);
        }
        public override void LoadData()
        {
            base.LoadData();
            data = DataBase.instance.Load(data);
        }
        public void UpgradeData()
        {
            data.valueLevel++;
            data.value += data.valueIncrease;
            float money = -data.valueUpgradeCost;
            data.valueUpgradeCost += data.valueNumberCostIncrease;
            EventManager.TriggerEvent(Events.CHANGE_MONEY, true, new object[] { money });
        }
    }
    [System.Serializable]
    public class UpgradeInfoData
    {
        public float value, valueIncrease;
        public int valueLevel, valueUpgradeCost, valueNumberCostIncrease;
    }
}