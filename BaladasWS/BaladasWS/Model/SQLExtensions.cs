using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BaladasWS.Model
{
    public static class SQLExtensions
    {
        public static string ToSql(this string value)
        {
            return string.Format("'{0}'", value);
        }

        public static string ToSql(this DateTime value)
        {
            string data = value.ToString("MM/dd/yyyy HH:mm:ss");
            return data.ToSql();
        }

        public static string ToSql(this TimeSpan value)
        {
            return value.ToString().ToSql();
        }
    
        public static string ToSql(this bool value)
        { 
            return (value ? "1": "0");
        }

        public static string ToSql(this double value)
        {
            return value.ToString("0.00", CultureInfo.InvariantCulture);
        }

        public static string ToSql(this int value)
        {
            return value.ToString();
        }
    }
}