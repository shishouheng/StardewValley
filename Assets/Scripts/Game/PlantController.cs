using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public enum PlantStates
	{
		Seed,Small,Ripe,Old
	}
	public partial class PlantController : ViewController,ISingleton
	{
		public static PlantController Instance => MonoSingletonProperty<PlantController>.Instance;
		public EasyGrid<IPlant> plants = new EasyGrid<IPlant>(10,10);
		void Start()
		{
			// Code Here
		}

		public void OnSingletonInit()
		{
			
		}
	}
}
