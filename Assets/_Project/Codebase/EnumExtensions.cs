using System;

namespace _Project.Codebase
{
    public static class EnumExtensions
    {
        public static bool TryGetNext<T>(this T myEnum, out T nextEnumValue) where T : Enum
        {
            var array = (T[])Enum.GetValues(myEnum.GetType());
            int index = Array.IndexOf(array, myEnum) + 1;

            nextEnumValue = index >= array.Length ? default : array[index];

            return index < array.Length;
        }
    }
}
