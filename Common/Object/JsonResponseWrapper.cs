using Market.Object.Constant;

namespace Market.Models.Common
{
    public class JsonResponseWrapper
    {
        public JsonResponseWrapper(ErrorCode resultCode, string message = "") {
            ResultCode = (int)resultCode;
            Message = message;
        }
        public int ResultCode { get; set; }
        public string Message { get; set; }
    }
}
