using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeForAll.APP.MiniGames.BlocksProgramming.Core 
{
    public class Grid
    {
        private int width;
        private int height;
        private float cellSize;
        private Vector3 originPosition;
        private int[,] gridArray;

        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new int[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width,height), Color.white, 100f);
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPosition;
        }

        private void GetXY(Vector3 worldPosition, out int x, out int y) 
        {            
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }

        public Vector3 GetXY(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);

            if(x >= 0 && y >= 0 && x < width && y < height)
                return GetWorldPosition(x, y);
            else
                return Vector3.negativeInfinity;
        }


    }

}
