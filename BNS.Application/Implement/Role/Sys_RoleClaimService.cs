using BNS.Application.Interface;
using BNS.Data.Entities;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BNS.Data.EntityContext;
using static BNS.Utilities.Enums;
using System.Data;
using BNS.Utilities;
using System.Net;
using AutoMapper;
using BNS.Resource;
using Microsoft.Extensions.Localization;
using BNS.Resource.LocalizationResources;
using Newtonsoft.Json;
using BNS.ViewModels.Responses;

namespace BNS.Application.Implement
{
    public class Sys_RoleClaimService : GenericRepository<Sys_RoleClaim>,
        ISys_RoleClaimService
    {
        private readonly IGenericRepository<Sys_RoleClaim> _genericRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        public Sys_RoleClaimService(BNSDbContext context,
            IGenericRepository<Sys_RoleClaim> genericRepository,
             IStringLocalizer<SharedResource> sharedLocalizer,
             IMapper mapper) : base(context)
        {
            _genericRepository = genericRepository;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }

        public async Task<ApiResult<Sys_RoleClaimResponseModel>> GetByDataId(Guid id, ERoleType type)
        {
            var result = new ApiResult<Sys_RoleClaimResponseModel>();

            var data = await _context.Sys_RoleClaims.Where(s => s.DataId == id && s.Type == type).FirstOrDefaultAsync();
            if (data != null)
            {
                result.data = new Sys_RoleClaimResponseModel();
                result.data.Id = data.Index;
                var roleClaims = JsonConvert.DeserializeObject<List<string>>(data.Roles);

                var dataRoles = await _context.Sys_Roles.Where(s => roleClaims.Contains(s.Id.ToString())).ToListAsync();
                result.data.Roles = dataRoles.Select(s => new Sys_RoleResponseModel
                {
                    Name = s.Name,
                    Note = s.Description,
                    Id = s.Id,
                    UpdatedDate = s.UpdatedDate,
                    CreatedDate = s.CreatedDate,
                    CreatedUserId = s.CreatedUser,
                    UpdatedUserId = s.UpdatedUser
                }).ToList();
            }
            return result;
        }

        public async Task<ApiResult<string>> Save(Sys_RoleClaimModel model)
        {
            var result = new ApiResult<string>();
            var saveResult = _context.Sys_RoleClaims.Where(s => s.DataId.Equals(model.DataId) &&
            s.Type == model.Type &&
            s.ShopIndex == model.ShopIndex &&
            s.IsDelete == null).FirstOrDefault();
            if (saveResult == null)
            {
                await _genericRepository.Add(new Sys_RoleClaim
                {
                    DataId = model.DataId,
                    Roles = model.RoleId != null ? JsonConvert.SerializeObject(model.RoleId) : string.Empty,
                    Type = model.Type,
                    CreatedDate = DateTime.UtcNow,
                    ShopIndex = model.ShopIndex,
                    CreatedUser = model.UserIndex
                });
                return result;
            }
            else
            {
                saveResult.Roles = model.RoleId != null ? JsonConvert.SerializeObject(model.RoleId) : string.Empty;
                saveResult.UpdatedDate = DateTime.UtcNow;
                saveResult.UpdatedUser = model.UserIndex;
                await _genericRepository.Update(saveResult);
            }
            return result;

        }

    }
}
