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
		public TileBase plantablePen;
		private void Start()
		{
			mShowGrid[0, 0] = new SoilData();

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					Ground.SetTile(new Vector3Int(i,j),plantablePen);
				}
			}
			mShowGrid.ForEach((x, y, data) =>
			{
				if (data!=null)
				{
					Soil.SetTile(new Vector3Int(x,y),pen);
				}
			});
		}
	}
}
