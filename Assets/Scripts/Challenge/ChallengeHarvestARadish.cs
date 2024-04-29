namespace ProjectIndieFarm
{
    public class ChallengeHarvestARadish: Challenge
    {
        public override string Name { get; } = "收获一个萝卜";
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return Global.Days.Value != startDate && Global.RipeAndHarvestRadishCountInCurrentDay.Value > 0;
        }

        public override void OnFinish()
        {
            
        }
    }
}