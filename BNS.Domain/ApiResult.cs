
using System;
using System.Net;
using static BNS.Utilities.Enums;

namespace BNS.Domain
{
    public class ApiResult<T>
    {
        public ApiResult()
        {
            status = HttpStatusCode.OK;
            errorCode = EErrorCode.Success.ToString();
            if (typeof(T).IsClass)
                data=   Activator.CreateInstance<T>();
        }
        public HttpStatusCode status { get; set; }
        public string errorCode { get; set; }

        public string title { get; set; }
        public int recordsTotal { get; set; }

        public T data { get; set; }
    }
}
