using UnityEngine;
namespace Game
{
	[CreateAssetMenu(fileName = "DataSo", menuName = "ScriptableObjects/DataSo")]
	[System.Serializable]
	public class DataSO : ScriptableObject
	{
		public virtual void SaveData()
		{

		}
		public virtual void LoadData()
		{

		}
		public virtual void ResetData()
		{

		}
    }
}