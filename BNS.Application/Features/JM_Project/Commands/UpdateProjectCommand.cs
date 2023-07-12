using BNS.Data.Entities.JM_Entities;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using System.Collections.Generic;
using BNS.Service.Implement.BaseImplement;
using AutoMapper;
using Newtonsoft.Json;
using BNS.Domain.Interface;

namespace BNS.Service.Features
{
    public class UpdateProjectCommand : UpdateRequestHandler<UpdateProjectRequest, JM_Project>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly IProjectService _projectService;

        public UpdateProjectCommand(IUnitOfWork unitOfWork,
            IMapper mapper,
            IProjectService projectService) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _projectService = projectService;
        }

        public override async Task<ApiResult<Guid>> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Project>().Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = LocalizedBackendMessages.MSG_ObjectNotExists;
                return response;
            }

            //var checkDuplicate = await _context.JM_Projects.Where(s => s.Name.Equals(request.Name)
            //&& s.Id != request.Id).FirstOrDefaultAsync();
            //if (checkDuplicate != null)
            //{
            //    response.errorCode = EErrorCode.IsExistsData.ToString();
            //    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
            //    return response;
            //}
            UpdateEntity<JM_Project>(dataCheck, request.ChangeFields, request.UserId);
            //dataCheck.UpdatedDate = DateTime.UtcNow;
            //dataCheck.UpdatedUserId = request.UserId;

            var team = request.ChangeFields.Where(s => s.Key.Equals("teams")).FirstOrDefault();
            var member = request.ChangeFields.Where(s => s.Key.Equals("members")).FirstOrDefault();
            var sprint = request.ChangeFields.Where(s => s.Key.Equals("sprints")).FirstOrDefault();

            if (team != null)
            {
                var value = JsonConvert.DeserializeObject<ChangeFieldTransferItem<Guid>>(team.Value.ToString());
                foreach (var item in value.AddValues)
                {
                    await _unitOfWork.Repository<JM_ProjectTeam>().AddAsync(new JM_ProjectTeam
                    {
                        ProjectId = request.Id,
                        TeamId = item,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                    });
                }
                if (value.DeleteValues != null && value.DeleteValues.Count > 0)
                {
                    var removeData = await _unitOfWork.Repository<JM_ProjectTeam>().Where(s => s.ProjectId == request.Id &&
                     value.DeleteValues.Contains(s.TeamId)).ToListAsync();
                    _unitOfWork.Repository<JM_ProjectTeam>().RemoveRange(removeData);
                }
            }


            if (member != null)
            {
                var value = JsonConvert.DeserializeObject<ChangeFieldTransferItem<Guid>>(member.Value.ToString());
                foreach (var item in value.AddValues)
                {
                    await _unitOfWork.Repository<JM_ProjectMember>().AddAsync(new JM_ProjectMember
                    {
                        ProjectId = request.Id,
                        UserId = item,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                    });
                }
                if (value.DeleteValues != null && value.DeleteValues.Count > 0)
                {
                    var removeData = await _unitOfWork.Repository<JM_ProjectMember>().Where(s => s.ProjectId == request.Id &&
                     value.DeleteValues.Contains(s.UserId)).ToListAsync();
                    _unitOfWork.Repository<JM_ProjectMember>().RemoveRange(removeData);
                }
            }

            if (sprint != null)
            {
                var values = JsonConvert.DeserializeObject<List<SprintRequest>>(sprint.Value.ToString());
                if (values != null && values.Count > 0)
                {
                    var valuesAdd = values.Where(s => s.RowStatus == ERowStatus.AddNew).ToList();
                    var valuesUpdate = values.Where(s => s.RowStatus == ERowStatus.Update && s.Id.HasValue).ToList();
                    var valuesDelete = values.Where(s => s.RowStatus == ERowStatus.Delete && s.Id.HasValue).Select(s => s.Id.Value).ToList();
                    foreach (var item in valuesAdd)
                    {
                        var projectPhase = _mapper.Map<JM_ProjectPhase>(item);
                        projectPhase.CreatedUserId = request.UserId;
                        projectPhase.CompanyId = request.CompanyId;
                        projectPhase.ProjectId = dataCheck.Id;
                        await _unitOfWork.Repository<JM_ProjectPhase>().AddAsync(projectPhase);
                        var childs = _projectService.GetAllChilds(projectPhase.Id, item, request.UserId, dataCheck.Id, request.CompanyId);
                        if (childs.Count > 0)
                        {
                            await _unitOfWork.Repository<JM_ProjectPhase>().AddRangeAsync(childs);
                        }
                    }
                    if (valuesDelete.Count > 0)
                    {
                        var removeData = await _unitOfWork.Repository<JM_ProjectPhase>().Where(s => valuesDelete.Contains(s.Id)).ToListAsync();
                        removeData.ForEach(s => s.IsDelete = true);
                        _unitOfWork.Repository<JM_ProjectPhase>().UpdateRange(removeData);
                    }
                    if (valuesUpdate.Count > 0)
                    {
                        var updatedIds = valuesUpdate.Select(s => s.Id.Value).ToList();
                        var updatesData = await _unitOfWork.Repository<JM_ProjectPhase>().Where(s => updatedIds.Contains(s.Id)).ToListAsync();
                        foreach (var item in updatesData)
                        {
                            var valueUpdate = valuesUpdate.Where(s => s.Id.Value == item.Id).FirstOrDefault();
                            if (valueUpdate != null)
                            {
                                _mapper.Map<SprintRequest, JM_ProjectPhase>(valueUpdate, item);
                            }
                        }
                        _unitOfWork.Repository<JM_ProjectPhase>().UpdateRange(updatesData);
                    }
                }
            }

            _unitOfWork.Repository<JM_Project>().Update(dataCheck);
            await _unitOfWork.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }

}
