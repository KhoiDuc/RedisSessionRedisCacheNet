using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Redis;
using Newtonsoft.Json;

namespace MyRedisSerializer
{
    public class JsonSerializer : ISerializer
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

        public byte[] Serialize(object data)
        {
            try
            {
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, _settings));
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                System.Diagnostics.Debug.WriteLine("Serialize error: " + ex.Message);
                throw;
            }
        }

        public object Deserialize(byte[] data)
        {
            try
            {
                if (data == null)
                {
                    return null;
                }
                return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), _settings);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                System.Diagnostics.Debug.WriteLine("Deserialize error: " + ex.Message);
                throw;
            }
        }
    }
}