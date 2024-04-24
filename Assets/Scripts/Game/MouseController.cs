using System;
using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public partial class MouseController : ViewController
    {
        private Grid mGrid;
        private Camera mMainCamera;
        private SpriteRenderer mSprite;

        private void Start()
        {
            mGrid = FindObjectOfType<GridController>().GetComponent<Grid>();
            mMainCamera = Camera.main;
            mSprite = GetComponent<SpriteRenderer>();
            mSprite.enabled=false;
        }

        private void Update()
        {
            var playerCellPos = mGrid.WorldToCell(Global.Player.Position());
            var worldPos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            var cellPos = mGrid.WorldToCell(worldPos);


            if (cellPos.x - playerCellPos.x == -1 || cellPos.y - playerCellPos.y == 1 ||
                cellPos.x - playerCellPos.x == 0 || cellPos.y - playerCellPos.y == 1 ||
                cellPos.x - playerCellPos.x == 1 || cellPos.y - playerCellPos.y == 1 ||
                cellPos.x - playerCellPos.x == 1 || cellPos.y - playerCellPos.y == 0 ||
                cellPos.x - playerCellPos.x == 1 || cellPos.y - playerCellPos.y == -1 ||
                cellPos.x - playerCellPos.x == 0 || cellPos.y - playerCellPos.y == -1 ||
                cellPos.x - playerCellPos.x == -1 || cellPos.y - playerCellPos.y == -1 ||
                cellPos.x - playerCellPos.x == -1 || cellPos.y - playerCellPos.y == 0
               )
            {
                var gridOriginPos = mGrid.CellToWorld(cellPos);
                gridOriginPos = gridOriginPos + mGrid.cellSize * 0.5f;
                transform.Position(gridOriginPos.x, gridOriginPos.y);
                mSprite.enabled=true;
            }
            else
            {
                mSprite.enabled=false;
            }
        }
    }
}