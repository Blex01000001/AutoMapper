using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Extensions
{
    internal class QuickSort
    {
        public static void Sort<T>(List<T> arrs, int LeftIndex, int RightIndex, string propName)
        {
            int start = LeftIndex;
            int end = RightIndex;
            T ini = arrs[LeftIndex];
            PropertyInfo propertyInfo = typeof(T).GetProperty(propName);

            while (end > start)
            {
                //while (end > start && arrs[end].Qty >= ini.Qty)
                while (end > start && (int)propertyInfo.GetValue(arrs[end]) >= (int)propertyInfo.GetValue(ini))
                {
                    end--;
                }
                // 直接交換不用再判斷，反正while不成立才會跳出
                (arrs[start], arrs[end]) = (arrs[end], arrs[start]);
                //while (end > start && arrs[start].Qty <= ini.Qty)
                while (end > start && (int)propertyInfo.GetValue(arrs[start]) <= (int)propertyInfo.GetValue(ini))
                {
                    start++;
                }
                (arrs[start], arrs[end]) = (arrs[end], arrs[start]);
            }

            if (start > LeftIndex)
            {
                Sort(arrs, LeftIndex, start - 1, propName);
            }

            if (end < RightIndex)
            {
                Sort(arrs, end + 1, RightIndex, propName);
            }
        }

    }
}
