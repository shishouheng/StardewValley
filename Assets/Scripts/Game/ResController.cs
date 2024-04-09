using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public GameObject seedPrefab;
		public GameObject waterPrefab;
		public GameObject smallPlantPrefab;
		public GameObject ripePrefab;

		public static ResController Instance => MonoSingletonProperty<ResController>.Instance;
		

		public void OnSingletonInit()
		{
			
		}
	}
}
