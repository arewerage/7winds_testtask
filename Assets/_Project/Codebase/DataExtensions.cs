using UnityEngine;

namespace _Project.Codebase
{
    public static class DataExtensions
    {
        public static string ToJson(this object data) =>
            JsonUtility.ToJson(data);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}
