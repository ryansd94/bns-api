
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Domain
{
    public class BaseAPiResult<T> : StatusCodeResult
    {
        public BaseAPiResult(int httpStatusCode = (int)HttpStatusCode.OK) : base(httpStatusCode)
        {
            errorCode = EErrorCode.Success.ToString();
        }
        public HttpStatusCode status { get; set; } = HttpStatusCode.OK;
        public string errorCode { get; set; } = EErrorCode.Success.ToString();
        public string title { get; set; }
        public int recordsTotal { get; set; }
    }
    public class ApiResult<T> : BaseAPiResult<T>
    {
        public ApiResult()
        {
            if (typeof(T).IsClass && typeof(T) != typeof(String))
                data = Activator.CreateInstance<T>();
        }
        public T data { get; set; } = Activator.CreateInstance<T>();
    }

    public class ApiResultList<T>: BaseAPiResult<T>
    {
        public ApiResultList()
        {
            if (typeof(T).IsClass && typeof(T) != typeof(String))
                data = Activator.CreateInstance<DynamicDataItem<T>>();
        }

        public DynamicDataItem<T> data { get; set; }
    }

    public class DynamicDataItem<T>
    {
        public DynamicDataItem()
        {
            Items = new List<T>();
        }
        public List<T> Items { get; set; }
    }
}
