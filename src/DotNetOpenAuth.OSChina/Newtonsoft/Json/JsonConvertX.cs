using System.IO;
using System.Runtime.Serialization.Json;
using DotNetOpenAuth;

namespace Newtonsoft.Json
{
    public static class JsonConvertX
    {
        public static T Deserialize<T>(Stream stream) where T : class
        {
            Requires.NotNull(stream, "stream");
            var dataContractJsonSerializer = new DataContractJsonSerializer(typeof (T));
            return (T) dataContractJsonSerializer.ReadObject(stream);
        }
    }
}