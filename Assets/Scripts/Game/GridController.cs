using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace ProjectIndieFarm
{
	public partial class GridController : ViewController
	{
		private EasyGrid<SoilData> mShowGrid = new EasyGrid<SoilData>(10, 10);
		public EasyGrid<SoilData> ShowGrid => mShowGrid;
		public TileBase pen;
		private void Start()
		{
			mShowGrid[1, 1] = new SoilData();
			mShowGrid[3, 5] = new SoilData();
			mShowGrid.ForEach((x, y, data) =>
			{
				if (data!=null)
				{
					tilemap.SetTile(new Vector3Int(x,y),pen);
				}
			});
		}
	}
}
