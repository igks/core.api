using System.Linq;
using System.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IMapper mapper, IDepartmentRepository departmenRepository, IUnitOfWork unitOfWork)
        {
            this.departmentRepository = departmenRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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
            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Create new department failed on save");
            }

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

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Update department failed on save");
            }

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

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Delete department failed on save");
            }

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