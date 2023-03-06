using Game.Utils;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Grid grid;
        [SerializeField, Min(1)] int gridMinSize = 1;
        public int score;
        GridGenerator generator;

        #region UNITY_METHODS
        void Start()
        {
            Initialize();
        }
        void OnEnable()
        {
            EventManager.StartListening(Events.PLACE_INTO_CELL, PlaceIntoCell);
        }
        void OnDisable()
        {
            EventManager.StopListening(Events.PLACE_INTO_CELL, PlaceIntoCell);
        }
        #endregion

        #region METHODS
        public void SetGridSize(string size)
        {
            int wantedSize = int.Parse(size);
            if (wantedSize >= gridMinSize)
                grid.properties.size = wantedSize;
            else
                EventManager.TriggerEvent(Events.POP_MIN_VALUE_WARNING, false);
        }
        public void Initialize()
        {
            generator = FindObjectOfType<GridGenerator>();
            ResetScore();
            if (grid == null)
                grid = FindObjectOfType<Grid>();
        }
        public void GenerateGrid()
        {
            if (grid == null)
                return;
            EventManager.TriggerEvent(Events.GENERATE_GRID, true, new object[] { grid });
            ResetScore();
        }
        private void ResetScore()
        {
            score = 0;
            EventManager.TriggerEvent(Events.UPDATE_SCORE_UI, true, new object[] { score.ToString() });
        }
        private void PlaceIntoCell(object[] obj)
        {
            if (grid == null)
                return;
            GameObject target = (GameObject)obj[0];
            int[] index = grid.FindSlotIndex(target);

            if (index == null || grid.placedObjects.Any(po => po.index.SequenceEqual(index)))
                return;

            grid.CellPlacement(index, target);

            bool scoreCondition = grid.CheckCellsForScore();
            if (scoreCondition)
            {
                score++;
                EventManager.TriggerEvent(Events.UPDATE_SCORE_UI, true, new object[] { score.ToString() });
                grid.ClearPlacedObjects();
            }
        }
        #endregion
    }
}