using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BaladasWS.Model
{
    public static class SqlDataReaderExtensions
    {
        public static Byte GetByte(this SqlDataReader reader, string name)
        {
            byte value = 0;

            try
            {
                value = reader.GetByte(reader.GetOrdinal(name));
            }
            catch { }

            return value;
        }

        public static Int16 GetInt16(this SqlDataReader reader, string name)
        {
            Int16 value = 0;

            try
            {
                value = reader.GetInt16(reader.GetOrdinal(name));
            }
            catch { }

            return value;
        }

        public static Int32 GetInteger(this SqlDataReader reader , string name)
        {
            int value = 0;

            try
            {
                value = reader.GetInt32(reader.GetOrdinal(name));
            }
            catch { }

            return value;
        }

        public static string GetString(this SqlDataReader reader, string name)
        {
            string value = string.Empty;

            try
            {
                value = reader.GetString(reader.GetOrdinal(name));
            }
            catch { }

            return value;
        }

        public static DateTime GetDateTime(this SqlDataReader reader, string name)
        {
            DateTime value = default(DateTime);

            try
            {
                value = reader.GetDateTime(reader.GetOrdinal(name));
            }
            catch { }

            return value;
        }

        public static double GetDouble(this SqlDataReader reader, string name)
        {
            double value = 0;

            try
            {
                value = reader.GetDouble(reader.GetOrdinal("name"));
            }
            catch { }

            return value;
        }

        public static bool GetBoolean(this SqlDataReader reader, string name)
        {
            bool value = false;

            try
            {
                value = reader.GetBoolean(reader.GetOrdinal(name));
            }
            catch { }

            return value;
        }

    }
}