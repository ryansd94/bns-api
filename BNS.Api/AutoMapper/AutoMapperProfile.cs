using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain.Commands;
using BNS.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BNS.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<JM_Task, TaskItem>()
                 .ForMember(s => s.UsersAssign,
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
            CreateMap<JM_Project, ProjectResponseItem>();
            CreateMap<JM_Team, TeamResponseItem>()
                .ForMember(s => s.TeamMembers, d => d.MapFrom(e => e.JM_AccountCompanys != null ? e.JM_AccountCompanys.Select(u => u.Id) : null))
                .ForMember(s => s.ParentName, d => d.MapFrom(u => u.TeamParent != null ? u.TeamParent.Name : string.Empty));
            CreateMap<JM_Template, TemplateResponseItem>().ForMember(s => s.Status, d => d.MapFrom(u => u.TemplateStatus.Select(r => r.Status)));
            CreateMap<JM_TaskType, TaskTypeItem>().ForMember(a => a.Template, opt => opt.MapFrom(src => src.Template));
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
        }
    }

    public static class Extensions
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> map,
        Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }

        public static void IgnoreSourceWhenDefault<TSource, TDestination>(this IMemberConfigurationExpression<TSource, TDestination, object> opt)
        {
            var destinationType = opt.DestinationMember.GetMemberType();
            object defaultValue = destinationType.GetTypeInfo().IsValueType ? Activator.CreateInstance(destinationType) : null;
            opt.Condition((src, dest, srcValue) => !Equals(srcValue, defaultValue));
        }

        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            if (memberInfo is MethodInfo)
                return ((MethodInfo)memberInfo).ReturnType;
            if (memberInfo is PropertyInfo)
                return ((PropertyInfo)memberInfo).PropertyType;
            if (memberInfo is FieldInfo)
                return ((FieldInfo)memberInfo).FieldType;
            return null;
        }
    }
}
