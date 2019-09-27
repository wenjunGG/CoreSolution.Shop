﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using CoreSolution.Dapper.Extension.Exception;

namespace CoreSolution.Dapper.Extension.Extension
{
    internal static class ReflectExtension
    {
        public static PropertyInfo[] GetProperties(this object obj)
        {
            return obj.GetType().GetProperties().Where(f => !Attribute.IsDefined(f, typeof(DatabaseGeneratedAttribute))).ToArray();
        }

        public static PropertyInfo GetKeyPropertity(this object obj)
        {
            var properties = obj.GetType().GetProperties().Where(a => a.GetCustomAttribute<KeyAttribute>() != null).ToArray();

            if (!properties.Any())
                throw new DapperExtensionException($"the {nameof(obj)} entity with no KeyAttribute Propertity");

            if (properties.Length > 1)
                throw new DapperExtensionException($"the {nameof(obj)} entity with greater than one KeyAttribute Propertity");

            return properties.First();
        }
        public static PropertyInfo GetKeyPropertity(this Type typeInfo)
        {
            var properties = typeInfo.GetProperties().Where(a => a.GetCustomAttribute<KeyAttribute>() != null).ToArray();

            if (!properties.Any())
                throw new DapperExtensionException($"the type {nameof(typeInfo.FullName)} entity with no KeyAttribute Propertity");

            if (properties.Length > 1)
                throw new DapperExtensionException($"the type {nameof(typeInfo.FullName)} entity with greater than one KeyAttribute Propertity");

            return properties.First();
        }
    }
}
