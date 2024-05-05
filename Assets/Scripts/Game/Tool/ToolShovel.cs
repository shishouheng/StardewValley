using QFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProjectIndieFarm
{
    public class ToolShovel: ITool
    {
        public bool Selectable(ToolData toolData)
        {
            return Global.CurrentTool.Value == Constant.TOOL_SHOVEL && 
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] == null;
        }
        
        public void Use(ToolData toolData)
        {
            toolData.SoilTilemap.SetTile(toolData.CellPos,toolData.Pen);
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] = new SoilData();
            AudioController.Get.SFXShoveDig.Play();
        }
    }
}