using System;
using System.Collections.Generic;

namespace _Project.Codebase
{
    public static class ListExtensions
    {
        private static readonly Random Rng = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            int index = list.Count;

            while (index > 1)
            {
                index--;
                int k = Rng.Next(index + 1);
                (list[index], list[k]) = (list[k], list[index]);
            }
        }
    }
}
