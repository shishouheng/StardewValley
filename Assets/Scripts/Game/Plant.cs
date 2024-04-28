using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public interface IPlant
    {
        GameObject GameObject { get; }
        PlantStates State { get; }
        
        int RipeDay { get; }

        void SetState(PlantStates states);

        void Grow(SoilData soilData);
    }

    public partial class Plant : ViewController, IPlant
    {
        public int xCell;
        public int yCell;
        private PlantStates mState = PlantStates.Seed;
        public PlantStates State => mState;
        public int RipeDay { get; private set; }
        public GameObject GameObject => gameObject;
        

        public void SetState(PlantStates newState)
        {
            Sprite currentSprite;
            if (mState != newState)
            {
                if (mState == PlantStates.Small && newState == PlantStates.Ripe)
                {
                    RipeDay = Global.Days.Value;
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
                    //切换到成熟状态
                    SetState(PlantStates.Ripe);
                }
            }
        }
    }
}