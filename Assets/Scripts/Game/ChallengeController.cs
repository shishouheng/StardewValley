using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
    public partial class ChallengeController : ViewController
    {
        public Font font;
        private GUIStyle mLabelStyle;

        /// <summary>
        /// 当天成熟并且收走的果实数量
        /// </summary>
        public static BindableProperty<int> RipeCountAndHarverstInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天收割的数量
        /// </summary>
        public static BindableProperty<int> HarverstCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天成熟并收割的胡萝卜数量
        /// </summary>
        public static BindableProperty<int> RipeAndHarvestRadishCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天收割萝卜的数量
        /// </summary>
        public static BindableProperty<int> RadishHarvestCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 收获过的果实数量
        /// </summary>
        public static int HarvestedFruitCount = 0;

        /// <summary>
        /// 收获过的萝卜数量
        /// </summary>
        public static int HarvestedRadishCount = 0;
        
        /// <summary>
        /// 所有挑战的列表
        /// </summary>
        public static List<Challenge> Challenges = new List<Challenge>()
        {
            new ChallengeHarvestOneFruit(),
            new ChallengeRipeAndHarverstTwoFruitsInOneDay(),
            new ChallengeRipeAndHarverstFiveFruitsInOneDay(),
            new ChallengeHarvestARadish(),
            new ChallengeRipeAndHarvestFruitAndRadishInADay(),
            new ChallengeHarvest10thFruit(),
            new ChallengeHarvest10thRadish(),
            new ChallengeFruitCountGreaterOrEqual10(),
            new ChallengeRadishCountGreaterOrEqual10()
        };

        /// <summary>
        /// 激活的挑战列表
        /// </summary>
        public static List<Challenge> ActiveChallenges = new List<Challenge>()
        {
        };

        /// <summary>
        /// 已经完成的挑战列表
        /// </summary>
        public static List<Challenge> FinishedChallenges = new List<Challenge>()
        {
        };

        /// <summary>
        /// 挑战完成事件
        /// </summary>
        public static EasyEvent<Challenge> OnChallengeFinished = new EasyEvent<Challenge>();

        private void Start()
        {
            mLabelStyle = new GUIStyle("Label") { font = font };

            //从所有挑战中随机获取一个挑战并加入到激活的挑战列表中
            var randomItem = ChallengeController.Challenges.GetRandomItem();
            ChallengeController.ActiveChallenges.Add(randomItem);
            //监听成熟的植物是否时当天成熟并且当天收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant is Plant)
                {
                    HarvestedFruitCount++;
                    ChallengeController.HarverstCountInCurrentDay.Value++;
                    if (plant.RipeDay == Global.Days.Value)
                    {
                        ChallengeController.RipeCountAndHarverstInCurrentDay.Value++;
                    }
                }
                else if (plant is PlantRadish)
                {
                    HarvestedRadishCount++;
                    ChallengeController.RipeAndHarvestRadishCountInCurrentDay.Value++;
                    if (plant.RipeDay == Global.Days.Value)
                    {
                        ChallengeController.RipeAndHarvestRadishCountInCurrentDay.Value++;
                        Debug.Log(ChallengeController.RadishHarvestCountInCurrentDay.Value);
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(960, 540);
            for (int i = 0; i < ChallengeController.ActiveChallenges.Count; i++)
            {
                var challenge = ChallengeController.ActiveChallenges[i];
                GUI.Label(new Rect(660, 20 + i * 26, 300, 24), challenge.Name, mLabelStyle);

                if (challenge.state == Challenge.States.Finished)
                {
                    GUI.Label(new Rect(660, 20 + i * 26, 300, 24), "<color=green>" + challenge.Name + "</color>",
                        mLabelStyle);
                }
            }

            for (int i = 0; i < ChallengeController.FinishedChallenges.Count; i++)
            {
                var challenge = ChallengeController.FinishedChallenges[i];
                GUI.Label(new Rect(660, 20 + (i + ChallengeController.ActiveChallenges.Count()) * 26, 300, 24),
                    "<color=green>" + challenge.Name + "</color>", mLabelStyle);
            }
        }

        private void Update()
        {
            bool hasFinishChallenge = false;
            foreach (var challenge in ChallengeController.ActiveChallenges)
            {
                if (challenge.state == Challenge.States.NotStart)
                {
                    challenge.startDate = Global.Days.Value;
                    challenge.OnStart();
                    challenge.state = Challenge.States.Started;
                }

                if (challenge.state == Challenge.States.Started)
                {
                    if (challenge.CheckFinish())
                    {
                        challenge.OnFinish();
                        challenge.state = Challenge.States.Finished;
                        ChallengeController.OnChallengeFinished.Trigger(challenge);
                        ChallengeController.FinishedChallenges.Add(challenge);
                        hasFinishChallenge = true;
                    }
                }
            }

            if (hasFinishChallenge)
            {
                ChallengeController.ActiveChallenges.RemoveAll(
                    challenge => challenge.state == Challenge.States.Finished);
            }

            if (ChallengeController.ActiveChallenges.Count() == 0 && ChallengeController.FinishedChallenges.Count !=
                ChallengeController.Challenges.Count)
            {
                //完成一个挑战后再从所有挑战中随机选取一个挑战加入到激活挑战列表中
                var randomItem = ChallengeController.Challenges.Where(c => c.state == Challenge.States.NotStart)
                    .ToList()
                    .GetRandomItem();
                ChallengeController.ActiveChallenges.Add(randomItem);
            }
        }
    }
}