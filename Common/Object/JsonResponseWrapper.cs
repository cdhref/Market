using Market.Object.Constant;

namespace Market.Models.Common
{
    public class JsonResponseWrapper
    {
        public JsonResponseWrapper(ErrorCode resultCode, string message = "", string resultString = "") {
            ResultCode = (int)resultCode;
            Message = message;
            ResultString = string.Format("{{\"result\":{0}}}", resultString);
        }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public string ResultString { get; set; }
    }
}
