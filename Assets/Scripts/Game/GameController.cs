using System;
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
            Global.OnChallengeFinished.Register(challenge => { Debug.Log(challenge.GetType().Name + "挑战完成"); });


            // Global.FruitCount.Register(fruitCount =>
            // {
            // 	if (fruitCount >= 1)
            // 	{
            // 		ActionKit.Delay(1.0f, (() =>
            // 		{
            // 			SceneManager.LoadScene("GamePass");
            // 		})).Start(this);
            // 	}
            // }).UnRegisterWhenGameObjectDestroyed(this);
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