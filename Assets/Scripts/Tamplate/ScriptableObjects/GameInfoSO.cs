using UnityEngine;
namespace Game
{
	[CreateAssetMenu(fileName = "GameInfoSO", menuName = "ScriptableObjects/GameInfoSO")]
    [System.Serializable]
    public class GameInfoSO : DataSO
	{
        public GameInfoData data = new GameInfoData();

        public override void ResetData()
        {
            data.currentLevel = 0;
            data.playerMoney = 0;
            DataBase.instance.Save(data);
        }
        public void IncreaseLevelCount()
        {
            data.currentLevel++;
        }
        public int GetLevelData()
        {
            return data.currentLevel;
        }
        public void SumPlayerMoney(int value)
        {
            data.playerMoney += value;
        }
        public float GetPlayerMoney()
        {
            return data.playerMoney;
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
    }
    [System.Serializable]
    public class GameInfoData
    {
        public int currentLevel;
        public int playerMoney;
    }
}