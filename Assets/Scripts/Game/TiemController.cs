using System;
using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class TiemController : ViewController
	{
		public static float Seconds = 0;
		void Start()
		{
			Seconds = 0;
		}

		private void Update()
		{
			Seconds += Time.deltaTime;
		}

		private void OnDestroy()
		{
			Debug.Log($"游戏通过用时{Seconds}s");
		}

		private void OnGUI()
		{
			IMGUIHelper.SetDesignResolution(640, 360);
			GUI.Label(new Rect(590,340,540,320),$"{(int)Seconds}s");
		}
	}
}
