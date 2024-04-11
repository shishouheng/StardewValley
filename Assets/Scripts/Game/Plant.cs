using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class Plant : ViewController
	{
		public int xCell;
		public int yCell;
		private PlantStates mState = PlantStates.Seed;
		public PlantStates state =>mState;

		public void SetState(PlantStates newState)
		{
			Sprite currentSprite;
			if (mState != newState)
			{
				mState = newState;
				//切换表现
				switch (newState)
				{
					case PlantStates.Small:
						currentSprite = ResController.Instance.smallPlantSprite;
						break;
					case PlantStates.Ripe:
						currentSprite = ResController.Instance.ripeSprite;
						break;
					case PlantStates.Seed:
						currentSprite = ResController.Instance.seedSprite;
						break;
					case PlantStates.Old:
						currentSprite = ResController.Instance.oldSprite;
						break;
					default:
						currentSprite = ResController.Instance.seedSprite;
						break;
				}

				GetComponent<SpriteRenderer>().sprite = currentSprite;
				//同步到SoilData中
				FindObjectOfType<GridController>().ShowGrid[xCell, yCell].PlantState = newState;
			}
		}
	}
}
