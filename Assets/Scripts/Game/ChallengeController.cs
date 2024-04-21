using System;
using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class ChallengeController : ViewController
	{
		private void OnGUI()
		{
			IMGUIHelper.SetDesignResolution(960,540);
			for (int i = 0; i < Global.Challenges.Count; i++)
			{
				var challenge = Global.Challenges[i];
				if (challenge.state == Challenge.States.Finished)
				{
					GUI.Label(new Rect(660,20+i*26,300,24),"<color=green>"+challenge.Name+"</color>");
				}
				else
				{
					GUI.Label(new Rect(660,20+i*26,300,24),challenge.Name);
				}
			}
		}
	}
}
