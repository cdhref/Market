using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Market.Common.Filter
{
    public class GlobalFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string requestURL = filterContext.HttpContext.Request.Url.AbsolutePath;
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;
            
            RequestLog log = new RequestLog(requestURL, httpMethod);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonLog = serializer.Serialize(log);

            // JSON 문자열 출력
            Trace.WriteLine(jsonLog);

            // 요청이 실행되기 전에 실행할 코드 작성
            base.OnActionExecuting(filterContext);
        }

    }
}