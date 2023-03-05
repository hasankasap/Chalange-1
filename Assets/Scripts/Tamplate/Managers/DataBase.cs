using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class DataBase : BaseSingleton<DataBase>
    {
        public DataSO[] datas;

        #region UNITY_METHODS
        #endregion

        #region METHODS
        public void Save<T>(T Data)
        {
            try
            {

                string filePath;

#if UNITY_EDITOR
                filePath = Application.dataPath + typeof(T).Name + ".json";
#else
                filePath = Path.Combine(Application.persistentDataPath, typeof(T).Name + ".json");
        
#endif
                string json = JsonUtility.ToJson(Data);
                File.WriteAllText(filePath, json);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        public T Load<T>(T Data) 
        {
            string filePath;

#if UNITY_EDITOR
            filePath = Application.dataPath + typeof(T).Name + ".json";
#else
                filePath = Path.Combine(Application.persistentDataPath, typeof(T).Name + ".json");
        
#endif

            if (!File.Exists(filePath))
                return Data;

            string jsonData = File.ReadAllText(filePath);
            Data = JsonUtility.FromJson<T>(jsonData);
            return Data;
        }
        
        #endregion
        
        #region ACTIONS
        public void SaveData()
        {
            if (datas.Length == 0)
                return;
            foreach (DataSO data in datas)
            {
                data.SaveData();
            }
        }
        public void LoadData()
        {
            if (datas.Length == 0)
                return;
            foreach (DataSO data in datas)
            {
                data.LoadData();
            }
        }
        public void ResetData()
        {
            if (datas.Length == 0)
                return;
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i].ResetData();
            }
        }
        #endregion
    }
}