using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Interface;
using BNS.Domain.Responses;
using BNS.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public NotifyResponse GetNotifyTaskResponse(Guid? objectId, Guid receivedAccountId,
            JM_Account userMenton, JM_Task task, ENotifyObjectType notifyObjectType)
        {
            var notifyResponse = new NotifyResponse
            {
                Id = Guid.NewGuid(),
                ObjectId = objectId,
                Type = notifyObjectType,
                UserReceivedId = receivedAccountId,
                CreatedDate = DateTime.UtcNow,
                Content = JsonConvert.SerializeObject(new NotifyTaskMention
                {
                    UserMention = new User
                    {
                        FullName = userMenton.FullName,
                        Id = userMenton.Id,
                        Image = userMenton.Image
                    },
                    TaskContent = new TaskNotify
                    {
                        Id = task.Id,
                        Title = task.Title,
                        TaskType = new TaskType
                        {
                            Color = task.TaskType?.Color,
                            Icon = task.TaskType?.Icon,
                        }
                    }
                })
            };
            return notifyResponse;
        }

        public NotifyResponse GetNotifyTaskResponse(Guid? objectId, Guid AccountId, JM_Account userMenton, JM_Task task, string taskTypeColor, string taskTypeIcon, ENotifyObjectType notifyObjectType)
        {
            task.TaskType = new JM_TaskType { Color = taskTypeColor, Icon = taskTypeIcon };
            return GetNotifyTaskResponse(objectId, AccountId, userMenton, task, notifyObjectType);
        }

        public List<string> GetUserMentionIdsWhenAddComments(string comment)
        {
            var userTags = HtmlHelper.GetDataAttributeFromHtmlString(comment, "data-id");
            return userTags;
        }

        public async Task<List<NotifyResponse>> GetUserMentionNotifyWhenAddComments(string comment, Guid taskId, Guid userId, Guid commentId)
        {
            var result = new List<NotifyResponse>();
            var task = await _unitOfWork.Repository<JM_Task>().Include(s => s.TaskType).Where(s => s.Id == taskId).FirstOrDefaultAsync();
            var userTags = HtmlHelper.GetDataAttributeFromHtmlString(comment, "data-id");
            var userMenton = await _unitOfWork.Repository<JM_Account>().Where(s => s.Id == userId).FirstOrDefaultAsync();
            if (task == null || userMenton == null)
            {
                return result;
            }
            foreach (var user in userTags)
            {
                if (userMenton.Id.ToString().Equals(user))
                    continue;
                var notifyResponse = GetNotifyTaskResponse(commentId, Guid.Parse(user), userMenton, task, ENotifyObjectType.TaskCommentMention);
                result.Add(notifyResponse);
            }
            return result;
        }

    }
}
