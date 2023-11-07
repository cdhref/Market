using Market.Common.Util;
using System.Diagnostics;
using System.Web.Mvc;

namespace Market.Common.Filter
{
    public class GlobalFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string requestURL = filterContext.HttpContext.Request.Url.AbsolutePath;
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;
            
            RequestLog log = new RequestLog(requestURL, httpMethod);
            string jsonLog = StringUtil.ObjectToJsonString(log);

            // JSON 문자열 출력
            Trace.WriteLine(jsonLog);

            // 요청이 실행되기 전에 실행할 코드 작성
            base.OnActionExecuting(filterContext);
        }

    }
}