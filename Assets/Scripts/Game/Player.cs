using System;
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
        public Font font;
        private GUIStyle mLabelStyle;

        private void Awake()
        {
            Global.Player = this;
        }

        private void Start()
        {
            mLabelStyle = new GUIStyle("Label"){font = this.font};
            
            Global.Days.Register(day =>
            {
                //天数变跟时重置当天的成熟果实数量为0
                ChallengeController.RipeCountAndHarverstInCurrentDay.Value = 0;
                ChallengeController.RipeAndHarvestRadishCountInCurrentDay.Value = 0;
                ChallengeController.HarverstCountInCurrentDay.Value = 0;
                ChallengeController.RipeAndHarvestRadishCountInCurrentDay.Value = 0;
                var soilDatas = FindObjectOfType<GridController>().ShowGrid;

                PlantController.Instance.plants.ForEach((x, y, plant) =>
                {
                    if (plant != null)
                    {
                        plant.Grow(soilDatas[x, y]);
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
            GUILayout.Label("天数" + Global.Days.Value,mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("果实" + Global.FruitCount.Value,mLabelStyle);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("胡萝卜" + Global.RadishCount.Value,mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label($"当前工具：{Global.CurrentTool.Value}",mLabelStyle);
            GUILayout.EndHorizontal();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Global.Days.Value++;
                AudioController.Get.SFXNextDay.Play();
            }

            var cellPos = grid.WorldToCell(transform.position);
            var gridData = FindObjectOfType<GridController>().ShowGrid;
            var tileWorldPos = grid.CellToWorld(cellPos);
            tileWorldPos.x += grid.cellSize.x * 0.5f;
            //tileWorldPos.y += grid.cellSize.y * 0.5f;

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
        }

        private void OnDestroy()
        {
            Global.Player = null;
        }
    }
}