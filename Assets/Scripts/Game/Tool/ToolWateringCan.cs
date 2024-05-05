using QFramework;

namespace ProjectIndieFarm
{
    public class ToolWateringCan: ITool
    {
        public string Name { get; set; } = "watering_can";

        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].Watered != true;
        }

        public void Use(ToolData toolData)
        {
            //浇水
            ResController.Instance.waterPrefab.Instantiate()
                .Position(toolData.GridCenterPos.x, toolData.GridCenterPos.y - 0.5f);
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].Watered = true;
            AudioController.Get.SFXWater.Play();
        }
    }
}