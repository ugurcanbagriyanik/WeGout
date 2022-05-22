
using System;
using System.Linq;
using System.Reflection;

namespace WeGout.Helpers
{
    public static class ObjectHelper
    {

        public static T CopyFrom<T, S>(T item, S source, params string[] excludedProperties) where T : new() where S : new()
        {
            if (item != null && source != null)
            {
                Type itemType = typeof(T);
                Type sourceType = typeof(S);

                foreach (var itemProp in itemType.GetProperties())
                {
                    if (excludedProperties != null && excludedProperties.Contains(itemProp.Name))
                    {
                        continue;
                    }

                    var sourceProp = sourceType.GetProperty(itemProp.Name);

                    if (sourceProp != null &&
                        sourceProp.PropertyType == itemProp.PropertyType)
                    {
                        itemProp.SetValue(item, sourceProp.GetValue(source));
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// Aynı tipteki iki class'ın property değerlerinin aynı olup olmadığını kontrol eder.
        /// </summary>
        public static bool ArePropertiesEqual<T>(T item, T source, params string[] excludedProperties) where T : new()
        {
            if (item != null && source != null)
            {
                Type type = typeof(T);

                foreach (var prop in type.GetProperties())
                {
                    if (excludedProperties != null && excludedProperties.Contains(prop.Name))
                    {
                        continue;
                    }

                    var itemValue = prop.GetValue(item);
                    var sourceValue = prop.GetValue(source);

                    if (itemValue == null && sourceValue == null)
                    {
                        continue;
                    }
                    else if (itemValue == null || sourceValue == null)
                    {
                        return false;
                    }
                    else if (prop.PropertyType == typeof(string) || ((dynamic)prop.PropertyType).IsValueType == true)
                    {
                        if (!itemValue.Equals(sourceValue))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        string itemValueJson = JsonHelper.Serialize(itemValue);
                        string sourceValueJson = JsonHelper.Serialize(sourceValue);

                        if (!string.Equals(itemValueJson, sourceValueJson))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verilen bir tipin struct olup olmadığını kontrol eder.
        /// int, int?, bool, bool?, string, DateTime, DateTime? vb. değer tipleri için true, class'lar için false döner.
        /// </summary>
        public static bool IsStruct(Type t)
        {
            if (t == typeof(string) || t == typeof(byte[]) || (t.GetTypeInfo().IsValueType == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
