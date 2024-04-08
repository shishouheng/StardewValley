using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectIndieFarm
{
    public partial class Player : ViewController
    {
        public Grid grid;
        public Tilemap tileMap;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(transform.position);
                var cellPos = grid.WorldToCell(transform.position);
                Debug.Log(cellPos);
                var gridData = FindObjectOfType<GridController>().ShowGrid;

                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    //没耕地
                    if (gridData[cellPos.x, cellPos.y] == null)
                    {
                        //开垦
                        tileMap.SetTile(cellPos, FindObjectOfType<GridController>().pen);
                        gridData[cellPos.x, cellPos.y] = new SoilData();
                    }
                    //耕地了 放种子
                    else if(gridData[cellPos.x,cellPos.y].hasPlant!=true)
                    {
                        var tileWorldPos = grid.CellToWorld(cellPos);
                        tileWorldPos.x += grid.cellSize.x * 0.5f;
                        //tileWorldPos.y += grid.cellSize.y * 0.5f;
                        ResController.Instance.seedPrefab.Instantiate().Position(tileWorldPos);
                        gridData[cellPos.x, cellPos.y].hasPlant = true;
                    }
                    else
                    {
                        
                    }
                }
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                var cellPos = grid.WorldToCell(transform.position);
                var gridData = FindObjectOfType<GridController>().ShowGrid;

                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    if (gridData[cellPos.x, cellPos.y]!=null)
                    {
                        tileMap.SetTile(cellPos, null);
                        gridData[cellPos.x, cellPos.y] = null;
                    }
                }
            }
        }
    }
}