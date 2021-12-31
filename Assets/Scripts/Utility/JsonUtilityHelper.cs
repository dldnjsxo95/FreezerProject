using System;
using UnityEngine;

namespace Futuregen
{
    public sealed class JsonUtilityHelper
    {
        [Serializable]
        private class ArrayWrapper<T>
        {
            public T Array;
        }

        public static T FromJsonArray<T>(string json)
        {
            string jsonString = "{\"Array\":" + json + '}';
            ArrayWrapper<T> wrapper = FromJson<ArrayWrapper<T>>(jsonString);
            return wrapper.Array;
        }

        public static T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static object FromJson(string json, Type type)
        {
            return JsonUtility.FromJson(json, type);
        }

        public static void FromJsonOverwrite(string json, object objectToOverwrite)
        {
            JsonUtility.FromJsonOverwrite(json, objectToOverwrite);
        }

        public static string ToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static string ToJson(object obj, bool prettyPrint)
        {
            return JsonUtility.ToJson(obj, prettyPrint);
        }
    }
}
