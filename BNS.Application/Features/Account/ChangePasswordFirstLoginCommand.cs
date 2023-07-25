using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Utilities;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class ChangePasswordFirstLoginCommand : IRequestHandler<ChangePasswordFirstLoginRequest, ApiResult<Guid>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IGenericRepository<JM_Account> _accountRepository;
        public ChangePasswordFirstLoginCommand(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
         IGenericRepository<JM_Account> accountRepository)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _accountRepository = accountRepository;
        }
        public async Task<ApiResult<Guid>> Handle(ChangePasswordFirstLoginRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var user = await _accountRepository.FirstOrDefaultAsync(s => s.Id == request.UserId);
            if (user == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return response;
            }
            user.PasswordHash=Ultility.MD5Encrypt(request.Password);
            response.data = user.Id;
            return response;
        }

    }
}
