﻿using BNS.Models;
using BNS.Models.Responses.Project;
namespace BNS.Domain.Queries
{
    public class GetJM_IssueByIdRequest : CommandByIdRequest<ApiResult<JM_IssueResponseItem>>
    {
    }
}
