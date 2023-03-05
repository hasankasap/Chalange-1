using Game.Utils;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Grid grid;
        [SerializeField, Min(1)] int gridMinSize = 1;
        GridGenerator generator;

        #region UNITY_METHODS
        void Start()
        {
            Initialize();
        }
        void OnEnable()
        {
            
        }
        void OnDisable()
        {
            
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
            if (grid == null)
                grid = FindObjectOfType<Grid>();
        }
        public void GenerateGrid()
        {
            if (grid == null)
                return;
            EventManager.TriggerEvent(Events.GENERATE_GRID, true, new object[] { grid });
        }

        [ButtonMethod]
        public void GenerateOnEditorGrid()
        {
            generator = FindObjectOfType<GridGenerator>();
            if (generator == null || grid == null)
                return;
            generator.GenerateGridOnScrene(new object[] { grid });
        }
        #endregion
    }
}