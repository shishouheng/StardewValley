using System.Linq;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace ProjectIndieFarm
{
    public partial class Player : ViewController
    {
        public Grid grid;
        public Tilemap tileMap;

        private void Start()
        {
            Global.Days.Register(day =>
            {
                //天数变跟时重置当天的成熟果实数量为0
                Global.RipeCountAndHarverstInCurrentDay.Value = 0;
                Global.HarverstCountInCurrentDay.Value = 0;
                var soilDatas = FindObjectOfType<GridController>().ShowGrid;

                PlantController.Instance.plants.ForEach((x, y, plant) =>
                {
                    if (plant)
                    {
                        if (plant.state == PlantStates.Seed)
                        {
                            if (soilDatas[x, y].Watered)
                            {
                                //切换到smallPlant状态
                                plant.SetState(PlantStates.Small);
                            }
                        }
                        else if (plant.state == PlantStates.Small)
                        {
                            if (soilDatas[x, y].Watered)
                            {
                                //切换到成熟状态
                                plant.SetState(PlantStates.Ripe);
                            }
                        }
                    }
                });

                soilDatas.ForEach(soilDatas =>
                {
                    if (soilDatas != null)
                    {
                        soilDatas.Watered = false;
                    }
                });
                foreach (var water in SceneManager.GetActiveScene().GetRootGameObjects()
                             .Where(gameObj => gameObj.name.StartsWith("Water")))
                {
                    water.DestroySelf();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(640, 340);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("天数" + Global.Days.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("果实" + Global.FruitCount.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label($"当前工具：{Global.CurrentTool.Value}");
            GUILayout.EndHorizontal();

            GUI.Label(new Rect(10, 336, 200, 24), "1.手  2.铁锹  3.种子  4.花洒");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Global.Days.Value++;
            }

            var cellPos = grid.WorldToCell(transform.position);
            var gridData = FindObjectOfType<GridController>().ShowGrid;
            var tileWorldPos = grid.CellToWorld(cellPos);
            tileWorldPos.x += grid.cellSize.x * 0.5f;
            //tileWorldPos.y += grid.cellSize.y * 0.5f;

            //选择框显示
            if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
            {
                if (Global.CurrentTool.Value == Constant.TOOL_SHOVEL && gridData[cellPos.x, cellPos.y] == null)
                {
                    TileSelectController.Instance.Position(tileWorldPos);
                    TileSelectController.Instance.Show();
                }
                else if (gridData[cellPos.x, cellPos.y] != null && gridData[cellPos.x, cellPos.y].HasPlant != true &&
                         Global.CurrentTool.Value == Constant.TOOL_SEED)
                {
                    TileSelectController.Instance.Position(tileWorldPos);
                    TileSelectController.Instance.Show();
                }
                else if (gridData[cellPos.x, cellPos.y] != null &&
                         gridData[cellPos.x, cellPos.y].Watered != true &&
                         Global.CurrentTool.Value == Constant.TOOL_WATERING_SCAN)
                {
                    TileSelectController.Instance.Position(tileWorldPos);
                    TileSelectController.Instance.Show();
                }
                else if (gridData[cellPos.x, cellPos.y] != null &&
                         gridData[cellPos.x, cellPos.y].HasPlant &&
                         gridData[cellPos.x, cellPos.y].PlantState == PlantStates.Ripe &&
                         Global.CurrentTool.Value == Constant.TOOL_HAND)
                {
                    TileSelectController.Instance.Position(tileWorldPos);
                    TileSelectController.Instance.Show();
                }
                else
                {
                    TileSelectController.Instance.Hide();
                }
            }

            else
            {
                TileSelectController.Instance.Hide();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    //没耕地
                    if (gridData[cellPos.x, cellPos.y] == null && Global.CurrentTool.Value == Constant.TOOL_SHOVEL)
                    {
                        //开垦
                        tileMap.SetTile(cellPos, FindObjectOfType<GridController>().pen);
                        gridData[cellPos.x, cellPos.y] = new SoilData();
                        AudioController.Get.SFXShoveDig.Play();
                    }

                    //耕地了 放种子
                    else if (gridData[cellPos.x, cellPos.y] != null &&
                             gridData[cellPos.x, cellPos.y].HasPlant != true &&
                             Global.CurrentTool.Value == Constant.TOOL_SEED)
                    {
                        var plantGameObject = ResController.Instance.plantPrefab.Instantiate().Position(tileWorldPos);
                        var plant = plantGameObject.GetComponent<Plant>();
                        plant.xCell = cellPos.x;
                        plant.yCell = cellPos.y;
                        PlantController.Instance.plants[cellPos.x, cellPos.y] = plantGameObject.GetComponent<Plant>();
                        gridData[cellPos.x, cellPos.y].HasPlant = true;
                    }


                    else if (gridData[cellPos.x, cellPos.y] != null &&
                             gridData[cellPos.x, cellPos.y].Watered != true &&
                             Global.CurrentTool.Value == Constant.TOOL_WATERING_SCAN)
                    {
                        //浇水
                        ResController.Instance.waterPrefab.Instantiate().Position(tileWorldPos);
                        gridData[cellPos.x, cellPos.y].Watered = true;
                    }


                    else if (gridData[cellPos.x, cellPos.y] != null &&
                             gridData[cellPos.x, cellPos.y].HasPlant &&
                             gridData[cellPos.x, cellPos.y].PlantState == PlantStates.Ripe &&
                             Global.CurrentTool.Value == Constant.TOOL_HAND)
                    {
                        //触发收割果实事件，然后会在GameController中持续检测该事件是否完成，完成了则结束改事件
                        Global.OnPlantHarvest.Trigger(PlantController.Instance.plants[cellPos.x, cellPos.y]);   
                        Destroy(PlantController.Instance.plants[cellPos.x, cellPos.y].gameObject);
                        gridData[cellPos.x, cellPos.y].HasPlant = false;
                        Global.HarverstCountInCurrentDay.Value++;
                        Global.FruitCount.Value++;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    if (gridData[cellPos.x, cellPos.y] != null)
                    {
                        tileMap.SetTile(cellPos, null);
                        gridData[cellPos.x, cellPos.y] = null;
                    }
                }
            }


            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("GamePass");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Global.CurrentTool.Value = Constant.TOOL_HAND;
                AudioController.Get.SFXTake.Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Global.CurrentTool.Value = Constant.TOOL_SHOVEL;
                AudioController.Get.SFXTake.Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Global.CurrentTool.Value = Constant.TOOL_SEED;
                AudioController.Get.SFXTake.Play();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Global.CurrentTool.Value = Constant.TOOL_WATERING_SCAN;
                AudioController.Get.SFXTake.Play();
            }
        }
    }
}