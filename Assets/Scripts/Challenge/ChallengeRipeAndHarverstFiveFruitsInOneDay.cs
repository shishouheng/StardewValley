namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarverstFiveFruitsInOneDay : Challenge
    {
        public override string Name { get; } = "收获同一天的五个果实";

        public override void OnStart()
        {
        }

        public override bool CheckFinish()
        {
            return Global.Days.Value != startDate && Global.RipeCountAndHarverstInCurrentDay.Value >= 5;
        }

        public override void OnFinish()
        {
        }
    }
}