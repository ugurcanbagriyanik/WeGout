// using Newtonsoft.Json;
using System.Text.Json;

namespace WeGout.Helpers
{
    /// <summary>
    /// Bir objeyi json serileştirme ve json'dan geri dönüştürme işlemlerini yapar.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Verilen objeyi json serileştirir.
        public static string Serialize<T>(T item)
        {
            return JsonSerializer.Serialize(item);
        }

        /// <summary>
        /// Verilen json string'den, belirtilen tipte bir obje oluşturup döner
        /// </summary>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
