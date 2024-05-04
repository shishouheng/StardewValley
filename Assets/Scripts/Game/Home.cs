using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class Home : ViewController
	{
		void Start()
		{
			// Code Here
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.name.StartsWith("Player"))
			{
				Global.Days.Value++;
				AudioController.Get.SFXNextDay.Play();
				other.PositionY(this.Position().y - 3);
			}
		}
	}
}
