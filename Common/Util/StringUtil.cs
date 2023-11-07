using System;
using System.Web.Script.Serialization;

namespace Market.Common.Util
{
    public class StringUtil
    {
        private static JavaScriptSerializer serializer;
        /// <summary>
        /// Objectをjson stringに変換する
        /// </summary>
        /// <returns>json string</returns>
        public static string ObjectToJsonString(object obj)
        {
            if(serializer == null)
            {
                serializer = new JavaScriptSerializer();
            }
            return serializer.Serialize(obj);
        }
    }
}