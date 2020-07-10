using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CORE.API.Controllers.Dto;
using CORE.API.Core.Models;

namespace CORE.API.Helpers.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, ViewDepartmentDto>();
            CreateMap<SaveDepartmentDto, Department>();

            CreateMap<FileList, ViewFileListDto>();
            CreateMap<SaveFileListDto, FileList>();

            CreateMap<User, ViewUserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<EditUserDto, User>();
            CreateMap<RegisterDto, User>();

            CreateMap<Employee, ViewEmployeeDto>();
            CreateMap<SaveEmployeeDto, Employee>();

            CreateMap<ModuleRight, ViewModuleDto>();
            CreateMap<SaveModuleDto, ModuleRight>();

            // map with converting string to array
            CreateMap<RoleGroup, ViewRoleGroupDto>()
                .ForMember(dest => dest.ModulesReadId, opt => opt.MapFrom(src => src.ModulesReadId.Split(",", StringSplitOptions.None).ToList()))
                .ForMember(dest => dest.ModulesWriteId, opt => opt.MapFrom(src => src.ModulesWriteId.Split(",", StringSplitOptions.None).ToList()));

            // map with converting array to string
            CreateMap<SaveRoleGroupDto, RoleGroup>()
                .ForMember(dest => dest.ModulesReadId, opt => opt.MapFrom(src => string.Join(",", src.ModulesReadId)))
                .ForMember(dest => dest.ModulesWriteId, opt => opt.MapFrom(src => string.Join(",", src.ModulesWriteId)));
        }
    }
}