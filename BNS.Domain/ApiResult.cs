
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Domain
{
    public class ApiResult<T> : StatusCodeResult
    {
        public ApiResult(int httpStatusCode = (int)HttpStatusCode.OK) : base(httpStatusCode)
        {
            errorCode = EErrorCode.Success.ToString();
            if (typeof(T).IsClass && typeof(T) != typeof(String))
                data = Activator.CreateInstance<T>();
        }
        public HttpStatusCode status { get; set; } = HttpStatusCode.OK;
        public string errorCode { get; set; } = EErrorCode.Success.ToString();
        public string title { get; set; }
        public int recordsTotal { get; set; }
        public T data { get; set; } = Activator.CreateInstance<T>();
    }
    public class ApiResultList<T>
    {
        public ApiResultList()
        {
            status = HttpStatusCode.OK;
            errorCode = EErrorCode.Success.ToString();
            if (typeof(T).IsClass && typeof(T) != typeof(String))
                data = Activator.CreateInstance<DynamicDataItem<T>>();
        }
        public HttpStatusCode status { get; set; }
        public string errorCode { get; set; }

        public string title { get; set; }
        public int recordsTotal { get; set; }

        public DynamicDataItem<T> data { get; set; }
    }

    public class DynamicDataItem<T>
    {
        public List<T> Items { get; set; }
    }
}
