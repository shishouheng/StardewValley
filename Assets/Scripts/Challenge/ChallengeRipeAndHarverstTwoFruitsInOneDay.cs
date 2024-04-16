using System.Collections.Generic;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarverstTwoFruitsInOneDay: Challenge,IUnRegisterList
    {
        public override void OnStart()
        {
            //监听成熟的植物是否时当天成熟并且当天收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.ripeDay == Global.Days.Value)
                {
                    Global.RipeCountAndHarverstInCurrentDay.Value++;
                }
            }).AddToUnregisterList(this);
        }

        public override bool CheckFinish()
        {
            return Global.RipeCountAndHarverstInCurrentDay.Value >= 2;
        }

        public override void OnFinish()
        {
            this.UnRegisterAll();
            // ActionKit.Delay(1.0f,(() =>
            // {
            //     SceneManager.LoadScene("GamePass");
            // })).StartGlobal();
        }

        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
    }
}