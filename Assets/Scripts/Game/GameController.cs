using System.Linq;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectIndieFarm
{
    public partial class GameController : ViewController
    {
        void Start()
        {
            //从所有挑战中随机获取一个挑战并加入到激活的挑战列表中
            var randomItem = Global.Challenges.GetRandomItem();
            Global.ActiveChallenges.Add(randomItem);
            //监听成熟的植物是否时当天成熟并且当天收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.RipeDay == Global.Days.Value)
                {
                    Global.RipeCountAndHarverstInCurrentDay.Value++;
                }
            }).UnRegisterWhenGameObjectDestroyed(this);


            //注册挑战完成的事件
            Global.OnChallengeFinished.Register(challenge =>
            {
                AudioController.Get.SFXChallengeFinish.Play();

                //某个挑战完成后检查是否所有挑战都已经完成，如果都完成则跳转到结束界面
                if (Global.Challenges.All(challenge => challenge.state == Challenge.States.Finished))
                {
                    ActionKit.Delay(0.5f, () => { SceneManager.LoadScene("GamePass"); }).Start(this);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            bool hasFinishChallenge = false;
            foreach (var challenge in Global.ActiveChallenges)
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
                        Global.OnChallengeFinished.Trigger(challenge);
                        Global.FinishedChallenges.Add(challenge);
                        hasFinishChallenge = true;
                    }
                }
            }

            if (hasFinishChallenge)
            {
                Global.ActiveChallenges.RemoveAll(challenge => challenge.state == Challenge.States.Finished);
            }

            if (Global.ActiveChallenges.Count() == 0&& Global.FinishedChallenges.Count!=Global.Challenges.Count)
            {
                //完成一个挑战后再从所有挑战中随机选取一个挑战加入到激活挑战列表中
                var randomItem = Global.Challenges.Where(c => c.state == Challenge.States.NotStart).ToList()
                    .GetRandomItem();
                Global.ActiveChallenges.Add(randomItem);
            }
        }
    }
}