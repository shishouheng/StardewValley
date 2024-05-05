using QFramework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace ProjectIndieFarm
{
    public interface ITool
    {
        bool Selectable(ToolData toolData);
        void Use(ToolData toolData);
    }

    public class ToolData
    {
        public EasyGrid<SoilData>ShowGrid { get; set; }
        public Vector3Int CellPos { get; set; }
        public Tilemap SoilTilemap { get; set; }
        public TileBase Pen { get; set; }
        public Vector3 GridCenterPos { get; set; }
    }
}