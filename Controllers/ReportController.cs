using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CORE.API.Controllers.Dto;
using CORE.API.Core.IRepository;
using Exp.API.Controllers.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CORE.API.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IMapper mapper;
        private readonly IDepartmentRepository departmentRepository;
        public ReportController(
        IMapper mapper,
        IDepartmentRepository departmenRepository
        )
        {
            this.departmentRepository = departmenRepository;
            this.mapper = mapper;
        }

        [HttpGet("department")]
        public async Task<IActionResult> GetDepartmentReport()
        {
            var departments = await departmentRepository.GetAll();

            var result = mapper.Map<IEnumerable<ViewDepartmentDto>>(departments);

            return Ok(result);
        }
    }
}