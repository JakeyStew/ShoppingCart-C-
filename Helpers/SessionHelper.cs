using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ShoppingKart.Helpers
{
    public static class SessionHelper
    {
        //Store the data in the session.
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        //Get the data in the session.
        public static T GetObjectFromJson<T> (this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
