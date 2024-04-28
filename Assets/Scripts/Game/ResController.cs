using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public GameObject waterPrefab;
		public GameObject plantPrefab;
		public GameObject plantRadishPrefab;	

		public Sprite seedSprite;
		public Sprite smallPlantSprite;
		public Sprite ripeSprite;
		public Sprite oldSprite;
		public Sprite seedRadishSprite;
		public Sprite smallPlantRadishSprite;
		public Sprite ripeRadishSprite;

		public static ResController Instance => MonoSingletonProperty<ResController>.Instance;
		

		public void OnSingletonInit()
		{
			
		}
	}
}
