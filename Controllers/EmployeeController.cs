using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CORE.API.Core.IRepository;
using CORE.API.Controllers.Dto;
using CORE.API.Core.Models;
using CORE.API.Helpers.Params;
using CORE.API.Helpers;

namespace CORE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeController(IMapper mapper, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.employeeRepository = employeeRepository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await employeeRepository.GetAll();
            var result = mapper.Map<IEnumerable<ViewEmployeeDto>>(employees);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var employee = await employeeRepository.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var result = mapper.Map<Employee, ViewEmployeeDto>(employee);

            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] EmployeeParams employeeParams)
        {

            var employees = await employeeRepository.GetPaged(employeeParams);

            var result = mapper.Map<IEnumerable<ViewEmployeeDto>>(employees);

            Response.AddPagination(employees.CurrentPage, employees.PageSize
                                  , employees.TotalCount, employees.TotalPages);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveEmployeeDto employeeDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = mapper.Map<SaveEmployeeDto, Employee>(employeeDto);
            employeeRepository.Add(employee);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Add new employee failed on save");
            }

            employee = await employeeRepository.GetById(employee.Id);
            var result = mapper.Map<Employee, ViewEmployeeDto>(employee);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee = mapper.Map(employeeDto, employee);
            employeeRepository.Update(employee);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Update employee failed on save");
            }

            employee = await employeeRepository.GetById(employee.Id);
            var result = mapper.Map<Employee, ViewEmployeeDto>(employee);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var employee = await employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            employeeRepository.Remove(employee);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Delete employee failed on save");
            }

            return Ok(id);
        }
    }
}