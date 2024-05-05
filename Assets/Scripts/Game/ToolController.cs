using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectIndieFarm
{
    public partial class ToolController : ViewController
    {
        private Grid mGrid;
        private GridController mGridController;
        private EasyGrid<SoilData> mShowGrid;
        private Camera mMainCamera;
        private SpriteRenderer mSprite;
        private Tilemap mTileMap;

        private void Awake()
        {
            Global.Tool = this;
        }

        private void Start()
        {
            mGridController = FindObjectOfType<GridController>();
            mTileMap = mGridController.Soil;
            mShowGrid = mGridController.ShowGrid;
            mGrid = mGridController.GetComponent<Grid>();
            mMainCamera = Camera.main;
            mSprite = GetComponent<SpriteRenderer>();
            mSprite.enabled = false;
        }

        
        private ToolData mToolData = new ToolData();

        private void Update()
        {
            var playerCellPos = mGrid.WorldToCell(Global.Player.Position());
            var worldPos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            //设置鼠标icon位置
            Icon.Position(worldPos.x + 0.5f, worldPos.y - 0.5f);
            var cellPos = mGrid.WorldToCell(worldPos);

            mSprite.enabled = false;
            //通过向量的方式判断鼠标是否在玩家位置周围
            Vector3Int offset = cellPos - playerCellPos;
            int absX = Mathf.Abs(offset.x);
            int absY = Mathf.Abs(offset.y);
            // 排除(0, 0)的情况
            bool isAdjacent = absX <= 1 && absY <= 1 /*&& (absX + absY > 0)*/;
            if (isAdjacent)
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    mToolData.ShowGrid = mShowGrid;
                    mToolData.CellPos = cellPos;
                    mToolData.Pen = mGridController.pen;
                    mToolData.SoilTilemap = mTileMap;
                    if (Global.CurrentTool.Value.Selectable(mToolData))
                    {
                        mToolData.GridCenterPos=ShowSelect(cellPos);

                        if (Input.GetMouseButton(0))
                        {
                            Global.CurrentTool.Value.Use(mToolData);
                        }
                    }
                }
            }
            else
            {
                mSprite.enabled = false;
            }
        }

        private void OnDestroy()
        {
            Global.Tool = null;
        }

        Vector3 ShowSelect(Vector3Int cellPos)
        {
            var gridOriginPos = mGrid.CellToWorld(cellPos);
            var gridCenterPos = gridOriginPos + mGrid.cellSize * 0.5f;
            transform.Position(gridCenterPos.x, gridCenterPos.y - 0.5f);
            mSprite.enabled = true;
            return gridCenterPos;
        }
    }
}