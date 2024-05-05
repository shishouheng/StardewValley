using UnityEngine;

namespace ProjectIndieFarm
{
    public class ToolHand: ITool
    {
        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant &&
                toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].PlantState == PlantStates.Ripe &&
                Global.CurrentTool.Value == Constant.TOOL_HAND;
        }

        public void Use(ToolData toolData)
        {
            //触发收割果实事件，然后会在GameController中持续检测该事件是否完成，完成了则结束改事件
            Global.OnPlantHarvest.Trigger(PlantController.Instance.plants[toolData.CellPos.x, toolData.CellPos.y]);

            if (PlantController.Instance.plants[toolData.CellPos.x, toolData.CellPos.y] as Plant)
            {
                Global.FruitCount.Value++;
            }
            else if (PlantController.Instance.plants[toolData.CellPos.x, toolData.CellPos.y] as PlantRadish)
            {
                Global.RadishCount.Value++;
            }

            Object.Destroy(PlantController.Instance.plants[toolData.CellPos.x, toolData.CellPos.y].GameObject);
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant = false;
            AudioController.Get.SFXHarvest.Play();
        }
    }
}