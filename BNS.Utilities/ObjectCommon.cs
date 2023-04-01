using System;
using System.Linq;
using System.Reflection;

namespace BNS.Utilities
{
    public static class ObjectCommon
    {
        /// <summary>
        /// Set the value of the property of the object by name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static void SetValueDynamic(object obj, string propertyName, object value)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValueItem(obj, value);
                return;
            }

            if (propertyName.Split('.').Length > 0)
            {
                string[] fieldNames = propertyName.Split(".");
                PropertyInfo currentProperty;
                object currentObject = obj;
                foreach (string fieldName in fieldNames)
                {
                    Type curentRecordType = currentObject.GetType();
                    currentProperty = curentRecordType.GetProperty(fieldName);

                    if (currentProperty != null)
                    {
                        var valueChild = currentProperty.GetValue(currentObject, null);
                        if (fieldNames.Last() == fieldName)
                        {
                            currentProperty.SetValueItem(currentObject, value);
                            return;
                        }

                        currentObject = valueChild;
                    }
                }
            }
        }

        public static void SetValueItem(this PropertyInfo propertyInfo, object obj, object value)
        {
            if (propertyInfo.PropertyType.IsEnum)
                propertyInfo.SetValue(obj, Enum.Parse(propertyInfo.PropertyType, value.ToString()));
            else
            {
                propertyInfo.SetValue(obj, value);
            }
        }
    }
}
