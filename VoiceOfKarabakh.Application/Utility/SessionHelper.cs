using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Text.Json;

namespace VoiceOfKarabakh.Application.Utility
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            string valueAsJson = JsonSerializer.Serialize(value);

            byte[] bytes = Encoding.ASCII.GetBytes(valueAsJson);
            session.Set(key, bytes);
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            byte[] objectAsBytes;

            if(session.TryGetValue(key, out objectAsBytes))
            {
                string json = Encoding.ASCII.GetString(objectAsBytes);
                T value = JsonSerializer.Deserialize<T>(json);

                return value;
            }

            return default(T);
        }
    }
}
