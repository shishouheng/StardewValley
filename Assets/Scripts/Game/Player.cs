using System;
using System.Linq;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
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
                                plant.SetState(PlantStates.Ripe);
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
            GUILayout.Label("浇水：E");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label($"当前工具：{Global.CurrentToolName.Value}");
            GUILayout.EndHorizontal();

            GUI.Label(new Rect(10, 336, 200, 24), "1.手  2.锄头");
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

            if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
            {
                TileSelectController.Instance.Position(tileWorldPos);
                TileSelectController.Instance.Show();
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
                    if (gridData[cellPos.x, cellPos.y] == null && Global.CurrentToolName.Value == "锄头")
                    {
                        //开垦
                        tileMap.SetTile(cellPos, FindObjectOfType<GridController>().pen);
                        gridData[cellPos.x, cellPos.y] = new SoilData();
                    }

                    return;
                    //耕地了 放种子
                    if (gridData[cellPos.x, cellPos.y].HasPlant != true)
                    {
                        var plantGameObject = ResController.Instance.plantPrefab.Instantiate().Position(tileWorldPos);
                        var plant = plantGameObject.GetComponent<Plant>();
                        plant.xCell = cellPos.x;
                        plant.yCell = cellPos.y;
                        PlantController.Instance.plants[cellPos.x, cellPos.y] = plantGameObject.GetComponent<Plant>();
                        gridData[cellPos.x, cellPos.y].HasPlant = true;
                    }
                    else if (gridData[cellPos.x, cellPos.y].HasPlant)
                    {
                        if (gridData[cellPos.x, cellPos.y].PlantState == PlantStates.Ripe)
                        {
                            Destroy(PlantController.Instance.plants[cellPos.x, cellPos.y].gameObject);
                            gridData[cellPos.x, cellPos.y].HasPlant = false;
                            Global.FruitCount.Value++;
                        }
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cellPos.x < 10 && cellPos.x >= 0 && cellPos.y < 10 && cellPos.y >= 0)
                {
                    //没耕地
                    if (gridData[cellPos.x, cellPos.y] != null)
                    {
                        if (gridData[cellPos.x, cellPos.y].Watered != true)
                        {
                            ResController.Instance.waterPrefab.Instantiate().Position(tileWorldPos);
                            gridData[cellPos.x, cellPos.y].Watered = true;
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("GamePass");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Global.CurrentToolName.Value = "手";
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Global.CurrentToolName.Value = "锄头";
            }
        }
    }
}