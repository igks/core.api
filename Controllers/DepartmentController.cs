using System.Linq;
using System.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CORE.API.Persistence;
using CORE.API.Core.IRepository;
using CORE.API.Controllers.Dto;
using CORE.API.Core.Models;
using CORE.API.Helpers.Params;
using CORE.API.Helpers;

namespace CORE.API.Controllers
{
    [Route("api/[controller]")]

    public class DepartmentController : Controller
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IMapper mapper, IDepartmentRepository departmenRepository, DataContext context)
        {
            this.departmentRepository = departmenRepository;
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await departmentRepository.GetAll();

            var result = mapper.Map<IEnumerable<ViewDepartmentDto>>(departments);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var department = await departmentRepository.GetById(id);

            if (department == null)
                return NotFound();

            var viewDepartmentDto = mapper.Map<Department, ViewDepartmentDto>(department);

            return Ok(viewDepartmentDto);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] DepartmentParams departmentParams)
        {

            var departments = await departmentRepository.GetPaged(departmentParams);

            var result = mapper.Map<IEnumerable<ViewDepartmentDto>>(departments);

            Response.AddPagination(departments.CurrentPage, departments.PageSize
                                  , departments.TotalCount, departments.TotalPages);


            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveDepartmentDto departmentDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = mapper.Map<SaveDepartmentDto, Department>(departmentDto);

            departmentRepository.Add(department);
            await context.SaveChangesAsync();

            department = await departmentRepository.GetById(department.Id);
            var result = mapper.Map<Department, ViewDepartmentDto>(department);

            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = await departmentRepository.GetById(id);


            if (department == null)
                return NotFound();

            department = mapper.Map(departmentDto, department);

            departmentRepository.Update(department);

            await context.SaveChangesAsync();

            department = await departmentRepository.GetById(department.Id);
            var result = mapper.Map<Department, ViewDepartmentDto>(department);

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            var department = await departmentRepository.GetById(id);

            if (department == null)
                return NotFound();

            departmentRepository.Remove(department);

            await context.SaveChangesAsync();

            return Ok(id);
        }

        // FIXME : make me to be reuseable
        private int getUserId()
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            if (idClaim != null)
            {
                var id = int.Parse(idClaim.Value);
                return id;
            }
            return -1;
        }

    }

}