using System;

namespace Market.Common.Filter
{
    public class RequestLog
    {
        public RequestLog() { }
        public RequestLog(string url, string method) {
            URL = url;
            Method = method;
            RequestAt = DateTime.Now.ToString();
        }
        public string URL { get; set; }
        public string Method { get; set; }
        public string RequestAt { get; set; }
    }
}