using QFramework;

namespace ProjectIndieFarm
{
    public class ToolSeed: ITool
    {
        public string Name { get; set; } = "seed";

        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant != true &&
                   Global.FruitSeedCount.Value>0;
        }

        public void Use(ToolData toolData)
        {
            Global.FruitSeedCount.Value--;
            //放种子
            //这里的y值如果不减去0.5f会出现在格子之外
            var plantGameObject = ResController.Instance.plantPrefab.Instantiate()
                .Position(toolData.GridCenterPos.x, toolData.GridCenterPos.y - 0.5f);
            var plant = plantGameObject.GetComponent<Plant>();
            plant.xCell = toolData.CellPos.x;
            plant.yCell = toolData.CellPos.y;
            PlantController.Instance.plants[toolData.CellPos.x, toolData.CellPos.y] = plant;
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant = true;
            AudioController.Get.SFXPutSeed.Play();
            
        }
    }
}