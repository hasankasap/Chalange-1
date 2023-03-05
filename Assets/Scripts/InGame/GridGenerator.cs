using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;
using MyBox;

namespace Game
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] Transform gridArea;
        Vector3 leftBorderPos, rightBorderPos, topBorder;
        float spacing;
        float cellSize;

        int gridSize;

        GameObject gridCellPrefab;
        GameObject gridParent;

        [SerializeField] Grid grid;

        #region UNITY_METHODS
        private void OnEnable()
        {
            EventManager.StartListening(Events.GENERATE_GRID, GenerateGridOnScrene);
        }

        private void OnDisable()
        {
            EventManager.StopListening(Events.GENERATE_GRID, GenerateGridOnScrene);
        }
        #endregion

        #region METHODS
        private void CalculateCellSize()
        {
            float distance = rightBorderPos.x - leftBorderPos.x;
            float totalSpacing = spacing * (gridSize + 1);
            cellSize = (distance - totalSpacing) / gridSize;
            topBorder.y -= ((cellSize / 2) + spacing);
        }
        private void CalculateBorders()
        {
            Camera camera = Camera.main;
            Vector3 border = camera.WorldToViewportPoint(gridArea.position);
            border.x = 0;
            leftBorderPos = camera.ViewportToWorldPoint(border);
            border.x = 1;
            rightBorderPos = camera.ViewportToWorldPoint(border);
            border.y = 1;
            topBorder = camera.ViewportToWorldPoint(border);

        }
        private void GenerateCell()
        {
            GameObject[,] createdCells = new GameObject[gridSize, gridSize];
            GameObject tempParent = new GameObject("Cell Parent");
            gridParent = Instantiate(tempParent, gridArea);
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Vector3 gridPos = gridArea.position;
                    float distance = (cellSize + spacing);
                    gridPos.y = topBorder.y - (distance * i);
                    gridPos.x = (spacing + leftBorderPos.x) + (distance * j);
                    GameObject temp = Instantiate(gridCellPrefab, gridParent.transform);
                    temp.transform.position = gridPos;
                    temp.transform.localScale = Vector3.one * cellSize;
                    createdCells[i,j] = temp;
                }
            }
            grid.cells = createdCells;
        }
        private void GenerateGridOnScrene(object[] obj)
        {
            grid= (Grid)obj[0];
            spacing = grid.properties.spacing;
            gridSize= grid.properties.size;
            gridCellPrefab = grid.properties.cellPrefab;
            CalculateBorders();
            CalculateCellSize();
            GenerateCell();
        }
        [ButtonMethod]
        public void Test()
        {
            spacing = grid.properties.spacing;
            gridSize = grid.properties.size;
            gridCellPrefab = grid.properties.cellPrefab;
            CalculateBorders();
            CalculateCellSize();
            GenerateCell();
        }
        #endregion
    }
}