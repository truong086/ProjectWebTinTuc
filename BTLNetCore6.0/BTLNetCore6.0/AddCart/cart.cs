using System.Text.Json;

namespace BTLNetCore6._0.AddCart
{
    public static class cart
    {
        public static string ToVND(this double giatien)
        {
            return $"{giatien:#,##0.00} Đ";
        }
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
