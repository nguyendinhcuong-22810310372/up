using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        if (value == null)
        {
            session.Remove(key); // nếu giá trị null thì xóa khỏi session
            return;
        }

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        string json = JsonConvert.SerializeObject(value, settings);
        session.SetString(key, json);
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);

        if (string.IsNullOrEmpty(value))
            return default;

        try
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        catch (Exception)
        {
            // nếu lỗi deserialize, xóa luôn session lỗi để tránh null sau này
            session.Remove(key);
            return default;
        }
    }
}