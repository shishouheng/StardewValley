using System.Collections;
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
    }
}

