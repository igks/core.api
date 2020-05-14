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
        }
    }
}