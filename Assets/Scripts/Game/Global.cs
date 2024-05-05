using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace ProjectIndieFarm
{
    public class Global : MonoBehaviour
    {
        /// <summary>
        /// 日期
        /// </summary>
        public static BindableProperty<int> Days = new BindableProperty<int>(1);

        /// <summary>
        /// 收获的果实数量
        /// </summary>
        public static BindableProperty<int> FruitCount = new BindableProperty<int>(0);

        /// <summary>
        /// 果实种子数量
        /// </summary>
        public static BindableProperty<int> FruitSeedCount = new BindableProperty<int>(5);

        /// <summary>
        /// 胡萝卜数量
        /// </summary>
        public static BindableProperty<int> RadishCount = new BindableProperty<int>(0);

        /// <summary>
        /// 胡萝卜种子数量
        /// </summary>
        public static BindableProperty<int> RadishSeedCount = new BindableProperty<int>(5);

        /// <summary>
        /// 当前工具
        /// </summary>
        public static BindableProperty<ITool> CurrentTool = new BindableProperty<ITool>(Constant.ToolHand);


        /// <summary>
        /// 植物收割事件
        /// </summary>
        public static EasyEvent<IPlant> OnPlantHarvest = new EasyEvent<IPlant>();

        public static Player Player = null;
        public static ToolController Tool = null;
    }

    
}