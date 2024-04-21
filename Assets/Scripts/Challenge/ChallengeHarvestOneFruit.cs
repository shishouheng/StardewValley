namespace ProjectIndieFarm
{
    public class ChallengeHarvestOneFruit: Challenge
    {
        public override string Name { get; } = "收获第一个果实";

        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return Global.Days.Value != startDate &&Global.HarverstCountInCurrentDay.Value > 0;
        }

        public override void OnFinish()
        {
            
        }
    }
}