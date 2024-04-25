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
            Debug.Log(cellPos);

            //通过向量的方式判断鼠标是否在玩家位置周围
            Vector3Int offset = cellPos - playerCellPos;
            int absX = Mathf.Abs(offset.x);
            int absY = Mathf.Abs(offset.y);
            // 排除(0, 0)的情况
            bool isAdjacent = absX <= 1 && absY <= 1 && (absX + absY > 0); 


            if (isAdjacent)
            {
                var gridOriginPos = mGrid.CellToWorld(cellPos);
                gridOriginPos.x += mGrid.cellSize.x * 0.5f;
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