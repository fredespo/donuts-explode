using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlmostEngine
{
    public static class ListExtras
    {
        public static void Resize<T>(this List<T> list, int size, T element = default(T))
        {
                // Debug.Log("Resize");
            int count = list.Count;

            if (size < count)
            {
                list.RemoveRange(size, count - size);
            }
            else if (size > count)
            {
                for (int i = 0; i <= size - list.Count; i++)
                    list.Add(element);  
                // Debug.Log("Add range");
                // if (size > list.Capacity)
                //     list.Capacity = size;

                // list.AddRange(Enumerable.Repeat(element, size - count));
            }
        }
    }
}