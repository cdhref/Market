using Market.Object.Constant;
using System;

namespace Market.Common.Util
{
    public class ErrorMessageUtil
    {
        /// <summary>
        /// system errorになった場合のエラーメッセージ
        /// </summary>
        /// <returns>system error message</returns>
        public static string GetServerErrorMessage(Exception e) {
            return string.Format(Message.serverErrorMessageFormat, e.Message);
        }
    }
}