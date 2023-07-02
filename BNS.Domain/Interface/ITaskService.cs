using BNS.Data.Entities.JM_Entities;
using BNS.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Interface
{
    public interface ITaskService
    {
        Task<List<NotifyResponse>> GetUserMentionNotifyWhenAddComments(string comment, Guid taskId, Guid userId, Guid commentId);
        List<string> GetUserMentionIdsWhenAddComments(string comment);
        NotifyResponse GetNotifyTaskResponse(Guid? objectId, Guid receivedAccountId,
            JM_Account userMenton, JM_Task task, ENotifyObjectType notifyObjectType);
        NotifyResponse GetNotifyTaskResponse(Guid? objectId, Guid receivedAccountId,
            JM_Account userMenton, JM_Task task, string taskTypeColor, string taskTypeIcon, ENotifyObjectType notifyObjectType);
    }
}
