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
            mTileMap = mGridController.tilemap;
            mShowGrid = mGridController.ShowGrid;
            mGrid = mGridController.GetComponent<Grid>();
            mMainCamera = Camera.main;
            mSprite = GetComponent<SpriteRenderer>();
            mSprite.enabled = false;
        }

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
            bool isAdjacent = absX <= 1 && absY <= 1 && (absX + absY > 0);
            if (isAdjacent)
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    //开垦
                    if (Global.CurrentTool.Value == Constant.TOOL_SHOVEL && mShowGrid[cellPos.x, cellPos.y] == null)
                    {
                        ShowSelect(cellPos);

                        if (Input.GetMouseButtonDown(0))
                        {
                            mTileMap.SetTile(cellPos, mGridController.pen);
                            mShowGrid[cellPos.x, cellPos.y] = new SoilData();
                            AudioController.Get.SFXShoveDig.Play();
                        }
                    }
                    else if (mShowGrid[cellPos.x, cellPos.y] != null && mShowGrid[cellPos.x, cellPos.y].HasPlant != true &&
                             Global.CurrentTool.Value == Constant.TOOL_SEED)
                    {
                        var gridCenterPos=ShowSelect(cellPos);

                        if (Input.GetMouseButtonDown(0))
                        {
                            //放种子
                            //这里的y值如果不减去0.5f会出现在格子之外
                            var plantGameObject = ResController.Instance.plantPrefab.Instantiate().Position(gridCenterPos.x,gridCenterPos.y-0.5f);
                            var plant = plantGameObject.GetComponent<Plant>();
                            plant.xCell = cellPos.x;
                            plant.yCell = cellPos.y;
                            PlantController.Instance.plants[cellPos.x, cellPos.y] = plant;
                            mShowGrid[cellPos.x, cellPos.y].HasPlant = true;
                            AudioController.Get.SFXPutSeed.Play();
                        }
                    }
                    else if (mShowGrid[cellPos.x, cellPos.y] != null &&
                             mShowGrid[cellPos.x, cellPos.y].Watered != true &&
                             Global.CurrentTool.Value == Constant.TOOL_WATERING_SCAN)
                    {
                        var gridCenterPos=ShowSelect(cellPos);
                        if (Input.GetMouseButtonDown(0))
                        {
                            //浇水
                            ResController.Instance.waterPrefab.Instantiate().Position(gridCenterPos.x,gridCenterPos.y-0.5f);
                            mShowGrid[cellPos.x, cellPos.y].Watered = true;
                            AudioController.Get.SFXWater.Play();
                        }
                    }
                    
                    else if (mShowGrid[cellPos.x, cellPos.y] != null &&
                             mShowGrid[cellPos.x, cellPos.y].HasPlant &&
                             mShowGrid[cellPos.x, cellPos.y].PlantState == PlantStates.Ripe &&
                             Global.CurrentTool.Value == Constant.TOOL_HAND)
                    {
                        ShowSelect(cellPos);

                        if (Input.GetMouseButtonDown(0))
                        {
                            //触发收割果实事件，然后会在GameController中持续检测该事件是否完成，完成了则结束改事件
                            Global.OnPlantHarvest.Trigger(PlantController.Instance.plants[cellPos.x, cellPos.y]);
                            Destroy(PlantController.Instance.plants[cellPos.x, cellPos.y].gameObject);
                            mShowGrid[cellPos.x, cellPos.y].HasPlant = false;
                            Global.HarverstCountInCurrentDay.Value++;
                            Global.FruitCount.Value++;
                            AudioController.Get.SFXHarvest.Play();
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
            var gridCenterPos=gridOriginPos + mGrid.cellSize * 0.5f;
            transform.Position(gridCenterPos.x, gridCenterPos.y-0.5f);
            mSprite.enabled = true;
            return gridCenterPos;
        }
    }
}