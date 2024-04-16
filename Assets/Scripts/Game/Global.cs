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
        /// 当前工具
        /// </summary>
        public static BindableProperty<string> CurrentTool = new BindableProperty<string>(Constant.TOOL_HAND);

        /// <summary>
        /// 当天成熟并且收走的果实数量
        /// </summary>
        public static BindableProperty<int> RipeCountAndHarverstInCurrentDay= new BindableProperty<int>(0);

        public static List<Challenge> Challenges = new List<Challenge>()
        {
            new ChallengeRipeAndHarverstTwoFruitsInOneDay()
        };

        /// <summary>
        /// 植物收割事件
        /// </summary>
        public static EasyEvent<Plant> OnPlantHarvest = new EasyEvent<Plant>();

        public static EasyEvent<Challenge> OnChallengeFinished = new EasyEvent<Challenge>();
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
            }

            return string.Empty;
        }
    }
}

    