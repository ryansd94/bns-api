using AutoMapper;
using BNS.Data.Entities;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
using BNS.ViewModels.Responses.Category;
using System;
using System.Reflection;

namespace BNS.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<CF_AreaModel, CF_Area>().ForMember(x => x.CreatedUser, opt => opt.Ignore());
            CreateMap<CF_DepartmentModel, CF_Department>();

            CreateMap<CF_Employee, CF_EmployeeModel>();
            CreateMap<CF_EmployeeModel, CF_Employee>();
            CreateMap<CF_Employee, CF_EmployeeResponseModel>();
            CreateMap<CF_Branch, CF_BranchResponseModel>();
            CreateMap<CF_BranchModel, CF_Branch>();
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
