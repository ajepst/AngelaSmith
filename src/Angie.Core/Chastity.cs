﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Angela.Core
{
    public class Chastity
    {
        internal static bool HasValue<T>(T instance, PropertyInfo property)
        {
            var value = property.GetValue(instance);
            bool valueSet = false;

            switch (property.PropertyType.Name.ToLower())
            {
                case "int32":
                    if ((Int32)value != default(Int32))
                        valueSet = true;
                    break;
                case "string":
                    if ((string)value != default(string))
                        valueSet = true;
                    break;
                default:
                    break;
            }

            return valueSet;
        }

    }
}