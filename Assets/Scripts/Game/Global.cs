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
    }
}

