using System;
using UnityEngine;
namespace DefaultNamespace
{
    public class TestDelegate: MonoBehaviour
    {
        private void Start()
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8 };
            CalculateAverage(nums, x => x % 2 == 0);
        }

        private int CalculateAverage(int[] nums, Func<int, bool> operation)
        {
            int temp = 0;
            int count = 0;
            foreach (var VARIABLE in nums)
            {
                if (operation(VARIABLE))
                {
                    temp += VARIABLE;
                    count++;
                }
            }

            return temp / count;
        }
    }
}