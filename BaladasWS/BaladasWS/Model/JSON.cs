using System.Web.Script.Serialization;

namespace BaladasWS.Model
{
    public class JSON
    {
        public static string Serialize(object obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(obj);
        }
    }
}