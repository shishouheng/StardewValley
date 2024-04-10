using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public enum PlantStates
	{
		Seed,Small,Ripe
	}
	public partial class PlantController : ViewController,ISingleton
	{
		public static PlantController Instance => MonoSingletonProperty<PlantController>.Instance;
		public EasyGrid<Plant> plants = new EasyGrid<Plant>(10,10);
		void Start()
		{
			// Code Here
		}

		public void OnSingletonInit()
		{
			
		}
	}
}
