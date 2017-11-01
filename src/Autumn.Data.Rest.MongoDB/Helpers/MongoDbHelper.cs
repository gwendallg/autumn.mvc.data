﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson.Serialization.Attributes;

namespace Autumn.Data.Rest.MongoDB
{
    public static class MongoDbHelper
    {
        private static readonly Dictionary<Type, PropertyInfo> Ids=new Dictionary<Type, PropertyInfo>();
        
        public static PropertyInfo GetId<T>()
        {
            lock (Ids)
            {
                if (Ids.ContainsKey(typeof(T))) return Ids[typeof(T)];
                var propertyInfo = typeof(T).GetProperties()
                    .Single(p => p.GetCustomAttribute<BsonIdAttribute>() != null);
                Ids.Add(typeof(T), propertyInfo);
                return Ids[typeof(T)];
            }
        }
    }
}