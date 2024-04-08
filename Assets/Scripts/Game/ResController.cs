using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public GameObject seedPrefab;

		public static ResController Instance => MonoSingletonProperty<ResController>.Instance;
		

		public void OnSingletonInit()
		{
			
		}
	}
}
