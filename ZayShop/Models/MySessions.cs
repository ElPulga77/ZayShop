using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace ZayShop.Models
{
    public static class MySessions
    {
        /*public static T Get<T>(ISession session, string key)
        {
            if (string.IsNullOrEmpty(session.GetString(key)))
            {
                session.SetString(key, JsonConvert.SerializeObject(null));
            }
            return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
        public static void Set<T>(ISession session, string key, T value)
        {
           *//* session.SetString(key, JsonConvert.SerializeObject(value));*//*
        }*/
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

    }
}
