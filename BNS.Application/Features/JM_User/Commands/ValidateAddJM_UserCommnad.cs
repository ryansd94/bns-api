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
    public class ValidateAddJM_UserCommnad : CommandBase<ApiResult<Guid>>
    {
        public class ValidateAddJM_UserCommnadRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Token { get; set; }
        }
        public class ValidateAddJM_UserCommnadHandler : IRequestHandler<ValidateAddJM_UserCommnadRequest, ApiResult<Guid>>
        {
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            protected readonly IElasticClient _elasticClient;
            protected readonly MyConfiguration _config;
            private readonly ICipherService _cipherService;
            private readonly IGenericRepository<JM_Account> _accountRepository;
            public ValidateAddJM_UserCommnadHandler(
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
            public async Task<ApiResult<Guid>> Handle(ValidateAddJM_UserCommnadRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var data = JsonConvert.DeserializeObject<JoinTeamResponse>(_cipherService.DecryptString(request.Token));
                if(data.Key != _config.Default.CipherKey)
                {
                    response.errorCode = EErrorCode.TokenNotValid.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_TokenNotValid];
                    return response;
                }
                var user = await _accountRepository.GetAsync(s => s.Email == data.EmailJoin);
                if (user != null)
                {
                    response.errorCode = EErrorCode.UserHasJoinTeam.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_ExistsUser];
                    return response;
                }
                return response;
            }

        }
    }
}