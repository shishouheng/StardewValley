using System.Collections.Generic;
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
            //注册挑战完成的事件
            ChallengeController.OnChallengeFinished.Register(challenge =>
            {
                AudioController.Get.SFXChallengeFinish.Play();

                //某个挑战完成后检查是否所有挑战都已经完成，如果都完成则跳转到结束界面
                if (ChallengeController.Challenges.All(challenge => challenge.state == Challenge.States.Finished))
                {
                    ActionKit.Delay(0.5f, () => { SceneManager.LoadScene("GamePass"); }).Start(this);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}