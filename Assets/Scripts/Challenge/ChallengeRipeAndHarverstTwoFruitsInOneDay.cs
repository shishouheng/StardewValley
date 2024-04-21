using System.Collections.Generic;
using QFramework;

namespace ProjectIndieFarm
{
    //一天成熟并且收获两枚果实的挑战
    public class ChallengeRipeAndHarverstTwoFruitsInOneDay : Challenge
    {
        public override string Name { get; } = "收获同一天的两个果实";

        public override void OnStart()
        {
        }

        public override bool CheckFinish()
        {
            return Global.Days.Value != startDate && Global.RipeCountAndHarverstInCurrentDay.Value >= 2;
        }

        public override void OnFinish()
        {
        }

        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
    }
}