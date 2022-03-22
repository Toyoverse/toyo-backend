using System;
using System.IO;
using System.Text;
using System.Text.Json;
using BackendToyo;

namespace BackendToyo
{
    public static class StringExtension
    {
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
        public static Stream ToStream(this string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public static byte[] ToByteArray(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}