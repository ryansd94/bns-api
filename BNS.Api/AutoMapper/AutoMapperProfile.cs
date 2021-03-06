using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain.Responses;
using System;
using System.Linq;
using System.Reflection;

namespace BNS.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<JM_Issue, JM_IssueResponseItem>();
            CreateMap<JM_Project, JM_ProjectResponseItem>();
            CreateMap<JM_Team, JM_TeamResponseItem>()
                .ForMember(s => s.TeamMembers, d => d.MapFrom(e => e.JM_TeamMembers != null ? e.JM_TeamMembers.Select(u => u.UserId) : null))
                .ForMember(s=>s.ParentName, d=>d.MapFrom(u=> u.TeamParent != null ? u.TeamParent.Name : string.Empty));
            CreateMap<JM_Template, JM_TemplateResponseItem>();
            CreateMap<JM_AccountCompany, JM_UserResponseItem>().ForMember
    (dest => dest.FullName, opt => opt.MapFrom(src => src.Status != Utilities.Enums.EUserStatus.ACTIVE ? string.Empty : src.JM_Account.FullName)); ;

        }
    }
    public static class Extensions
    {
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
