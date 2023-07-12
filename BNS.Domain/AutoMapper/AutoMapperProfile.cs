using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain.Commands;
using BNS.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BNS.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<JM_Task, TaskItem>()
                 .ForMember(s => s.UsersAssignId,
                 d => d.MapFrom(e => e.AssignUserId != null ?
                 new List<Guid> { e.AssignUserId.Value } :
                 (e.TaskUsers != null ? e.TaskUsers.Select(s => s.UserId).ToList() : null)))
                 .ForMember(s => s.CreatedUser, d => d.MapFrom(e => new User
                 {
                     FullName = e.User.FullName,
                     Image = e.User.Image
                 }))
                 .ForMember(s => s.Status, d => d.MapFrom(e => new StatusResponseItem
                 {
                     Name = e.Status != null ? e.Status.Name : "",
                     Color = e.Status != null ? e.Status.Color : "",
                 }))
                 .ForMember(s => s.TaskType, d => d.MapFrom(e => new TaskType
                 {
                     Name = e.TaskType.Name,
                     Color = e.TaskType.Color,
                     Icon = e.TaskType.Icon,
                 }))
                 .ForMember(s => s.TaskCustomColumnValues, d => d.MapFrom(e => e.TaskCustomColumnValues != null ?
                e.TaskCustomColumnValues.Select(s => new TaskCustomColumnValue { Value = s.Value, CustomColumnId = s.CustomColumnId }).ToArray() : null))
                 .ForMember(s => s.Tags, d => d.MapFrom(e => e.TaskTags != null ?
                e.TaskTags.Where(s => !s.IsDelete && !s.Tag.IsDelete).Select(s => new TagItem
                {
                    Id = s.TagId,
                    Name = s.Tag.Name,
                }).ToArray() : null));

            CreateMap<SprintRequest, JM_ProjectPhase>();
            CreateMap<JM_ProjectPhase, SprintReponse>();
            CreateMap<JM_Project, ProjectResponseItem>()
                .ForMember(s => s.Teams, o => o.MapFrom(x => x.JM_ProjectTeams.Select(s => s.TeamId).ToList()))
                .ForMember(s => s.Members, o => o.MapFrom(x => x.JM_ProjectMembers.Select(s => s.UserId).ToList()))
                .ForMember(s => s.Sprints, o => o.MapFrom(x => x.Sprints.OrderBy(s => s.CreatedDate).Where(s => s.ParentId == null && s.IsDelete == false)));
            CreateMap<JM_Team, TeamResponseItem>()
                .ForMember(s => s.TeamMembers, d => d.MapFrom(e => e.JM_AccountCompanys != null ? e.JM_AccountCompanys.Select(u => u.Id) : null))
                .ForMember(s => s.ParentName, d => d.MapFrom(u => u.TeamParent != null ? u.TeamParent.Name : string.Empty));
            CreateMap<JM_Template, TemplateResponseItem>().ForMember(s => s.Status, d => d.MapFrom(u => u.TemplateStatus.OrderByDescending(s => s.Status.IsStatusStart).OrderBy(s => s.Status.IsStatusEnd).Select(r => new StatusItemResponse { Id = r.StatusId, Name = r.Status.Name, Color = r.Status.Color, IsStatusEnd = r.Status.IsStatusEnd, IsStatusStart = r.Status.IsStatusStart })));
            CreateMap<JM_TaskType, TaskTypeItem>()
                .ForMember(a => a.Template, opt => opt.MapFrom(src => src.Template));
            CreateMap<JM_AccountCompany, UserResponseItem>().ForMember
    (dest => dest.FullName, opt => opt.MapFrom(src => src.Status == Utilities.Enums.EUserStatus.WAILTING_CONFIRM_MAIL ? string.Empty : src.Account.FullName)); ;
            CreateMap<JM_Status, StatusResponseItem>();
            CreateMap<JM_Tag, TagResponseItem>();
            CreateMap<SYS_FilterConfig, FilterConfigResponseItem>();
            CreateMap<CreateTaskRequest, JM_Task>();
            CreateMap<TaskDefaultData, JM_Task>();
            CreateMap<UpdateTaskTypeRequest, JM_TaskType>();
            CreateMap<UpdateStatusRequest, JM_Status>();
            CreateMap<JM_CustomColumn, CustomColumnsResponseItem>();
            CreateMap<FileItem, JM_File>();
            CreateMap<JM_Account, User>();
            CreateMap<JM_Comment, CommentResponseItem>().ForMember(s => s.User, d => d.MapFrom(e => new User
            {
                Id = e.User.Id,
                FullName = e.User.FullName,
                Image = e.User.Image,
            })).ForMember(s => s.UpdatedTime, d => d.MapFrom(s => s.UpdatedDate.ToString()));
            CreateMap<CreatePriorityRequest, JM_Priority>().ForMember(s => s.CreatedUserId, d => d.MapFrom(e => e.UserId));
            CreateMap<UpdatePriorityRequest, JM_Priority>().ForMember(s => s.UpdatedUserId, d => d.MapFrom(e => e.UserId));
            CreateMap<JM_Priority, PriorityResponseItem>();
            CreateMap<CreateViewPermissionRequest, SYS_ViewPermission>().ForMember(s => s.CreatedUserId, d => d.MapFrom(e => e.UserId));
            CreateMap<ViewPermissionAction, SYS_ViewPermissionAction>().ForMember(s => s.Controller, d => d.MapFrom(e => e.View)); ;
            CreateMap<ViewActionItem, SYS_ViewPermissionActionDetail>();
            CreateMap<SYS_ViewPermission, ViewPermissionResponseItem>();
            CreateMap<SYS_ViewPermission, ViewPermissionByIdResponse>()
                .ForMember(s => s.Permission, d => d.MapFrom(e => e.ViewPermissionActions.Select(a => new ViewPermissionAction
                {
                    View = a.Controller.ToString(),
                    Actions = a.ViewPermissionActionDetails.Select(b => new ViewActionItem
                    {
                        Key = b.Key.ToString(),
                        Value = b.Value != null ? b.Value.Value : false
                    }).ToList()
                }).ToList()))
                .ForMember(s => s.Objects, d => d.MapFrom(e => e.ViewPermissionObjects.Select(a => new ViewPermissionObjectResponse
                {
                    Id = a.ObjectId,
                    ObjectType = a.ObjectType
                }).ToList()));
            CreateMap<JM_NotifycationUser, NotifyResponse>();
            CreateMap<ReadNotifyRequest, JM_NotifycationUser>();
            CreateMap<CreateTemplateRequest, JM_Template>().ForMember(s => s.CreatedUserId, d => d.MapFrom(e => e.UserId));
            CreateMap<CreateProjectRequest, JM_Project>().ForMember(s => s.CreatedUserId, d => d.MapFrom(e => e.UserId));
            CreateMap<CreateStatusRequest, JM_Status>().ForMember(s => s.CreatedUserId, d => d.MapFrom(e => e.UserId));
            CreateMap<JM_Status, StatusItemResponse>();
        }
    }
}
