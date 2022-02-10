﻿using BNS.Application.Interface;
using BNS.Data.Entities.JM_Entities;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using BNS.ViewModels.Responses;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Features
{
    public class AddJM_UserCommand : CommandBase<ApiResult<Guid>>
    {
        public class AddJM_UserCommandRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Token { get; set; }
            [Required]
            public string FullName { get; set; }
            [Required]
            public string Password { get; set; }
        }
        public class AddJM_UserCommandHandler : IRequestHandler<AddJM_UserCommandRequest, ApiResult<Guid>>
        {
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            protected readonly IElasticClient _elasticClient;
            protected readonly MyConfiguration _config;
            private readonly ICipherService _cipherService;
            private readonly IGenericRepository<JM_Account> _accountRepository;
            public AddJM_UserCommandHandler(
             IStringLocalizer<SharedResource> sharedLocalizer,
             IOptions<MyConfiguration> config,
            ICipherService CipherService,
             IElasticClient elasticClient,
             IGenericRepository<JM_Account> accountRepository)
            {
                _sharedLocalizer = sharedLocalizer;
                _elasticClient = elasticClient;
                _config = config.Value;
                _cipherService = CipherService;
                _accountRepository = accountRepository;
            }
            public async Task<ApiResult<Guid>> Handle(AddJM_UserCommandRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var data = JsonConvert.DeserializeObject<JoinTeamResponse>(_cipherService.DecryptString(request.Token));
                if (data.Key != _config.Default.CipherKey)
                {
                    response.errorCode = EErrorCode.TokenNotValid.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_TokenNotValid];
                    return response;
                }
                var user = await _accountRepository.GetDefaultAsync(s => s.Email == data.EmailJoin);
                if (user != null)
                {
                    response.errorCode = EErrorCode.UserHasJoinTeam.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_ExistsUser];
                    return response;
                }

                var userId = Guid.NewGuid();
                user = new JM_Account
                {
                    Id = userId,
                    UserName = data.EmailJoin,
                    Email = data.EmailJoin,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = data.UserRequest,
                    IsDelete = false,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    IsMainAccount = true,
                    FullName = request.FullName,
                };
                await _accountRepository.AddAsync(user);
                return response;
            }

        }
    }
}