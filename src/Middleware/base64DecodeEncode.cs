using System;

namespace BackendToyo.Middleware
{
    public static class base64DecodeEncode
    {
        public static string Base64Encode(string text) {
            var textInBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textInBytes);
        }

        public static string Base64Decode(string base64EncodedData) {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}