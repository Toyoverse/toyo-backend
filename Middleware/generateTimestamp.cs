using System;

namespace BackendToyo.Middleware
{
    public static class generateTimestamp
    {
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}