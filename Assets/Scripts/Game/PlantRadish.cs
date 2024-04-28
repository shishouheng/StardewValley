using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class PlantRadish : ViewController,IPlant
	{
		public int xCell;
		public int yCell;
		private PlantStates mState = PlantStates.Seed;
		public PlantStates State =>mState;
		public GameObject GameObject => gameObject;

		/// <summary>
		/// 成熟的日期（第几天）
		/// </summary>
		public int ripeDay = -1;

		public void SetState(PlantStates newState)
		{
			Sprite currentSprite;
			if (mState != newState)
			{
				if (mState == PlantStates.Small && newState == PlantStates.Ripe)
				{
					ripeDay = Global.Days.Value;
				}
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
						currentSprite = ResController.Instance.seedRadishSprite;
						break;
					case PlantStates.Old:
						currentSprite = ResController.Instance.oldSprite;
						break;
					default:
						currentSprite = ResController.Instance.seedRadishSprite;
						break;
				}

				GetComponent<SpriteRenderer>().sprite = currentSprite;
				//同步到SoilData中
				FindObjectOfType<GridController>().ShowGrid[xCell, yCell].PlantState = newState;
			}
		}

		//胡萝卜处于small状态的天数
		private int mSmallStateDays = 0;
		public void Grow(SoilData soilDatas)
		{
			if (State == PlantStates.Seed)
			{
				if (soilDatas.Watered)
				{
					//切换到smallPlant状态
					SetState(PlantStates.Small);
				}
			}
			else if (State == PlantStates.Small)
			{
				if (soilDatas.Watered)
				{
					mSmallStateDays++;
					if (mSmallStateDays == 2)
					{
						//切换到成熟状态
						SetState(PlantStates.Ripe);
					}
				}
			}
		}
	}
}
