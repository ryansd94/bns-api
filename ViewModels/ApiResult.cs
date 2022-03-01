
using System.Net;
using static BNS.Utilities.Enums;

namespace BNS.Models
{
    public class ApiResult<T>
    {
        public ApiResult()
        {
            status = HttpStatusCode.OK;
            errorCode = EErrorCode.Success.ToString();
        }
        public HttpStatusCode status { get; set; }
        public string errorCode { get; set; }
        public string type { get; set; }

        public string title { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }

        public T data { get; set; }
    }
}
