namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarvestFruitAndRadishInADay: Challenge
    {
        public override string Name { get; } = "同一天收获当天成熟的果实和萝卜";
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return Global.Days.Value != startDate && ChallengeController.RipeCountAndHarverstInCurrentDay.Value >= 1 &&
                   ChallengeController.RipeAndHarvestRadishCountInCurrentDay.Value >= 1;
        }

        public override void OnFinish()
        {
            
        }
    }
}