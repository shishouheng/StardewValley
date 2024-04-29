using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace  ProjectIndieFarm
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
        /// 胡萝卜数量
        /// </summary>
        public static BindableProperty<int> RadishCount = new BindableProperty<int>();
        
        /// <summary>
        /// 当前工具
        /// </summary>
        public static BindableProperty<string> CurrentTool = new BindableProperty<string>(Constant.TOOL_HAND);

        /// <summary>
        /// 当天成熟并且收走的果实数量
        /// </summary>
        public static BindableProperty<int> RipeCountAndHarverstInCurrentDay= new BindableProperty<int>(0);

        /// <summary>
        /// 当天收割的数量
        /// </summary>
        public static BindableProperty<int> HarverstCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天成熟并收割的胡萝卜数量
        /// </summary>
        public static BindableProperty<int> RipeAndHarvestRadishCountInCurrentDay = new BindableProperty<int>(0);

        /// <summary>
        /// 当天收割萝卜的数量
        /// </summary>
        public static BindableProperty<int> RadishHarvestCountInCurrentDay = new BindableProperty<int>(0);
        
        /// <summary>
        /// 所有挑战的列表
        /// </summary>
        public static List<Challenge> Challenges = new List<Challenge>()
        {
            new ChallengeHarvestOneFruit(),
            new ChallengeRipeAndHarverstTwoFruitsInOneDay(),
            new ChallengeRipeAndHarverstFiveFruitsInOneDay(),
            new ChallengeHarvestARadish(),
            new ChallengeRipeAndHarvestFruitAndRadishInADay()
        };

        /// <summary>
        /// 激活的挑战列表
        /// </summary>
        public static List<Challenge> ActiveChallenges = new List<Challenge>()
        {

        };

        /// <summary>
        /// 已经完成的挑战列表
        /// </summary>
        public static List<Challenge> FinishedChallenges = new List<Challenge>()
        {

        };

        /// <summary>
        /// 植物收割事件
        /// </summary>
        public static EasyEvent<IPlant> OnPlantHarvest = new EasyEvent<IPlant>();

        /// <summary>
        /// 挑战完成事件
        /// </summary>
        public static EasyEvent<Challenge> OnChallengeFinished = new EasyEvent<Challenge>();

        public static Player Player = null;
        public static ToolController Tool = null;
    }

    /// <summary>
    /// 常量
    /// </summary>
    public class Constant
    {
        public const string TOOL_HAND = "hand";
        public const string TOOL_SHOVEL = "shovel";
        public const string TOOL_SEED = "seed";
        public const string TOOL_WATERING_SCAN = "watering_scan";

        /// <summary>
        /// 萝卜
        /// </summary>
        public const string TOOL_SEED_RADISH = "seed_radish";

        public static string DisplayName(string tool)
        {
            switch (tool)
            {
                case TOOL_HAND:
                    return "手";
                case TOOL_SHOVEL:
                    return "铁锹";
                case TOOL_SEED:
                    return "种子";
                case TOOL_WATERING_SCAN:
                    return "花洒";
                case TOOL_SEED_RADISH:
                    return "萝卜";
            }

            return string.Empty;
        }
    }
}

    