using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJsonString(this object value, Formatting formatting = Formatting.None)
        {
            try
            {
                return JObject.FromObject(value).ToString(formatting);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось преобразовать объект в json", e);
            }
        }


        /// <summary>
        /// Определяет, является ли коллекция нулевой или не содержит элементов.
        /// </summary>
        /// <typeparam name="T">Тип перечисления</typeparam>
        /// <param name="enumerable">Перечисление, которое может быть нулевым или пустым.</param>
        /// <returns>True - перечисление является нулевым или пустым. Иначе -  false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            switch (enumerable)
            {
                case null:
                    return true;
                case string aString:
                    return string.IsNullOrWhiteSpace(aString);
                // If this is a list, use the Count property for efficiency. 
                // The Count property is O(1) while IEnumerable.Count() is O(N).
                case ICollection<T> collection:
                    return collection.Count < 1;
                default:
                    return !enumerable.Any();
            }
        }
    }
}