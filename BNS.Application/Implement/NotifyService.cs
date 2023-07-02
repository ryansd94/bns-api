using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Interface;
using BNS.Domain.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNS.Service.Implement
{
    public class NotifyService : INotifyService
    {
        protected readonly INotifyGateway _notifyGateway;
        private readonly IUnitOfWork _unitOfWork;

        public NotifyService(INotifyGateway notifyGateway,
            IUnitOfWork unitOfWork)
        {
            _notifyGateway = notifyGateway;
            _unitOfWork = unitOfWork;
        }

        private async Task InsertDataBeforeSendNotify(List<NotifyResponse> lstNotify, Guid userId, Guid companyId)
        {
            var listNotifycation = new List<JM_NotifycationUser>();
            foreach (var item in lstNotify)
            {
                var notifycation = new JM_NotifycationUser
                {
                    Content = item.Content,
                    ObjectId = item.ObjectId,
                    Type = item.Type,
                    CompanyId = companyId,
                    UpdatedUserId = userId,
                    CreatedUserId = userId,
                    UserReceivedId = item.UserReceivedId
                };
                item.Id = notifycation.Id;
                listNotifycation.Add(notifycation);
            }
            await _unitOfWork.Repository<JM_NotifycationUser>().AddRangeAsync(listNotifycation);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SendNotify(List<NotifyResponse> lstNotify, Guid userId, Guid companyId)
        {
            await InsertDataBeforeSendNotify(lstNotify, userId, companyId);
            _notifyGateway.SendNotify(lstNotify);
        }
    }
}
