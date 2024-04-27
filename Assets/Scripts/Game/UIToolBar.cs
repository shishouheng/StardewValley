using System;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ProjectIndieFarm
{
	public partial class UIToolBar : ViewController
	{
		private void Start()
		{
			HideAllSelect();
			Btn1select.Show();
			Global.Tool.Icon.sprite = Btn1Icon.sprite;
			Btn1.onClick.AddListener((() => ChangeTool(Constant.TOOL_HAND,Btn1select,Btn1Icon.sprite)));
			Btn2.onClick.AddListener((() => ChangeTool(Constant.TOOL_SHOVEL,Btn2select,Btn2Icon.sprite)));
			Btn3.onClick.AddListener((() => ChangeTool(Constant.TOOL_SEED,Btn3select,Btn3Icon.sprite)));
			Btn4.onClick.AddListener((() => ChangeTool(Constant.TOOL_WATERING_SCAN,Btn4select,Btn4Icon.sprite)));
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				ChangeTool(Constant.TOOL_HAND,Btn1select,Btn1Icon.sprite);
			}

			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				ChangeTool(Constant.TOOL_SHOVEL,Btn2select,Btn2Icon.sprite);
			}

			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				ChangeTool(Constant.TOOL_SEED,Btn3select,Btn3Icon.sprite);
			}

			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				ChangeTool(Constant.TOOL_WATERING_SCAN,Btn4select,Btn4Icon.sprite);
			}
		}

		void HideAllSelect()
		{
			Btn1select.Hide();
			Btn2select.Hide();
			Btn3select.Hide();
			Btn4select.Hide();
		}
		void ChangeTool(string tool,Image selectImage,Sprite icon)
		{
			Global.CurrentTool.Value = tool;
			AudioController.Get.SFXTake.Play();
	
			HideAllSelect();
			Global.Tool.Icon.sprite = icon;
			selectImage.Show();
		}
	}
}
