using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EngineDesigner.Common.Serialization
{
    public static class BinarySerializer
    {
        private static BinaryFormatter binaryFormatter = new BinaryFormatter();


        public static byte[] Serialize(object _serializableObject)
        {
            MemoryStream _memoryStream = new MemoryStream();
            binaryFormatter.Serialize(_memoryStream, _serializableObject);

            return _memoryStream.ToArray();
        }
        public static object Deserialize(byte[] _byteArray)
        {
            MemoryStream _memoryStream = new MemoryStream(_byteArray);
            object _target = binaryFormatter.Deserialize(_memoryStream);
            return _target;
        }
        public static T Deserialize<T>(byte[] _byteArray)
        {
            MemoryStream _memoryStream = new MemoryStream(_byteArray);
            T _target = (T)binaryFormatter.Deserialize(_memoryStream);
            return _target;
        }

    }
}
