using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace  ProjectIndieFarm
{
    public class Global : MonoBehaviour
    {
        /// <summary>
        /// 默认从第一天开始算
        /// </summary>
        public static BindableProperty<int> Days = new BindableProperty<int>(1);
    }
}

