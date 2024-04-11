using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public GameObject waterPrefab;
		public GameObject plantPrefab;

		public Sprite seedSprite;
		public Sprite smallPlantSprite;
		public Sprite ripeSprite;
		public Sprite oldSprite;

		public static ResController Instance => MonoSingletonProperty<ResController>.Instance;
		

		public void OnSingletonInit()
		{
			
		}
	}
}
