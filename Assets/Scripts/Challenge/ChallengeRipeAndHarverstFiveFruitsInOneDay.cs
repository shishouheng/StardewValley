namespace ProjectIndieFarm
{
    public class ChallengeRipeAndHarverstFiveFruitsInOneDay: Challenge
    {
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return Global.FruitCount.Value >= 5;
        }

        public override void OnFinish()
        {
            
        }
    }
}