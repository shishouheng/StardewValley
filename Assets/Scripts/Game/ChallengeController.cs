using System;
using System.Linq;
using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class ChallengeController : ViewController
	{
		public Font font;
		private GUIStyle mLabelStyle;

		private void Start()
		{
			mLabelStyle = new GUIStyle("Label") { font = font };
		}

		private void OnGUI()
		{
			IMGUIHelper.SetDesignResolution(960,540);
			for (int i = 0; i < Global.ActiveChallenges.Count; i++)
			{
				var challenge = Global.ActiveChallenges[i];
				GUI.Label(new Rect(660,20+i*26,300,24),challenge.Name,mLabelStyle);

				if (challenge.state == Challenge.States.Finished)
				{
					GUI.Label(new Rect(660,20+i*26,300,24),"<color=green>"+challenge.Name+"</color>",mLabelStyle);
				}
			}
			
			for (int i = 0; i < Global.FinishedChallenges.Count; i++)
			{
				var challenge = Global.FinishedChallenges[i];
				GUI.Label(new Rect(660,20+(i+Global.ActiveChallenges.Count())*26,300,24),"<color=green>"+challenge.Name+"</color>",mLabelStyle);	
			}
		}
	}
}
