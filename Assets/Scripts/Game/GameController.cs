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
            //监听成熟的植物是否时当天成熟并且当天收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.ripeDay == Global.Days.Value)
                {
                    Global.RipeCountAndHarverstInCurrentDay.Value++;
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
            //挑战完成则输出xxx挑战完成
            Global.OnChallengeFinished.Register(challenge =>
            {
                Debug.Log(challenge.GetType().Name + "挑战完成");

                //某个挑战完成后检查是否所有挑战都已经完成，如果都完成则跳转到结束界面
                if (Global.Challenges.All(challenge => challenge.state == Challenge.States.Finished))
                {
                    ActionKit.Delay(0.5f, () =>
                    {
                        SceneManager.LoadScene("GamePass");
                    }).Start(this);
                }
            });
        }

        private void Update()
        {
            foreach (var challenge in
                     Global.Challenges.Where(challenge => challenge.state != Challenge.States.Finished))
            {
                if (challenge.state == Challenge.States.NotStart)
                {
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
                    }
                }
            }
        }
    }
}