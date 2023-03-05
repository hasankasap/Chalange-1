using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
	public class LevelSO : ScriptableObject
	{
		public Level levelPrefab;

		public Level GetLevelPrefab()
		{
			return levelPrefab;
		}
	}
}