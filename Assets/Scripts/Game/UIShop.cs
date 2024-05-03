using UnityEngine;
using QFramework;

namespace ProjectIndieFarm
{
	public partial class UIShop : ViewController
	{
		void Start()
		{
			Global.FruitCount.RegisterWithInitValue(fruitCount =>
			{
				if (fruitCount >= 1)
				{
					BtnBuyFruitSeed.Show();
				}
				else
				{
					BtnBuyFruitSeed.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.RadishCount.RegisterWithInitValue(radishCount =>
			{
				if (radishCount >= 1)
				{
					BtnBuyRadishSeed.Show();
				}
				else
				{
					BtnBuyRadishSeed.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			BtnBuyFruitSeed.onClick.AddListener((() =>
			{
				Global.FruitCount.Value -= 1;
				Global.FruitSeedCount.Value += 2;
			}));
			
			BtnBuyRadishSeed.onClick.AddListener((() =>
			{
				Global.RadishCount.Value -= 1;
				Global.RadishSeedCount.Value += 2;
			}));
		}
	}
}
