using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Suricato.Serialization
{
    public class Serialize
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object Clone(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream buffer = new MemoryStream())
            {
                formatter.Serialize(buffer, obj);
                buffer.Position = 0;
                return formatter.Deserialize(buffer);
            }
        }
    }
}