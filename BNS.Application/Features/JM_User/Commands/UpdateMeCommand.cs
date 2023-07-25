using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Data.Entities.JM_Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BNS.Domain.Responses;
using BNS.Utilities;

namespace BNS.Service.Features
{
    public class UpdateMeCommand : IRequestHandler<UpdateMeRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly MyConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateMeCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _config = config.Value;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateMeRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var account = await _unitOfWork.Repository<JM_Account>().Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (account == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return response;
            }
            account.UpdatedDate = DateTime.UtcNow;
            account.UpdatedUser = request.UserId;
            var settings = new SettingResponse();
            if (!string.IsNullOrEmpty(account.Setting))
            {
                settings = JsonConvert.DeserializeObject<SettingResponse>(account.Setting);
            }
            foreach (var item in request.Configs)
            {
                ObjectCommon.SetValueDynamic(settings, item.Key, item.Value);
            }
            account.Setting = JsonConvert.SerializeObject(settings);

            _unitOfWork.Repository<JM_Account>().Update(account);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
