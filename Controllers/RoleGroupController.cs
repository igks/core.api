using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CORE.API.Controllers.Dto;
using CORE.API.Core.IRepository;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;
using Microsoft.AspNetCore.Mvc;

namespace CORE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleGroupController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRoleGroupRepository groupRepository;
        private readonly IUnitOfWork unitOfWork;

        public RoleGroupController(IMapper mapper, IRoleGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.groupRepository = groupRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await groupRepository.GetAll();
            var result = mapper.Map<IEnumerable<ViewRoleGroupDto>>(groups);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await groupRepository.GetById(id);
            if (group == null)
            {
                return NotFound();
            }

            var result = mapper.Map<RoleGroup, ViewRoleGroupDto>(group);
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] RoleGroupParams roleGroupParams)
        {
            var groups = await groupRepository.GetPaged(roleGroupParams);

            var result = mapper.Map<IEnumerable<ViewRoleGroupDto>>(groups);

            Response.AddPagination(groups.CurrentPage, groups.PageSize, groups.TotalCount, groups.TotalPages);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveRoleGroupDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = mapper.Map<SaveRoleGroupDto, RoleGroup>(groupDto);
            groupRepository.Add(group);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: "Create new role group failed on save");
            }

            group = await groupRepository.GetById(group.Id);
            var result = mapper.Map<RoleGroup, ViewRoleGroupDto>(group);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveRoleGroupDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = await groupRepository.GetById(id);
            if (group == null)
            {
                return NotFound();
            }

            group = mapper.Map(groupDto, group);
            groupRepository.Update(group);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: "Update role group failed on save");
            }

            group = await groupRepository.GetById(group.Id);
            var result = mapper.Map<RoleGroup, ViewRoleGroupDto>(group);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var group = await groupRepository.GetById(id);
            if (group == null)
            {
                return NotFound();
            }

            groupRepository.Remove(group);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: "Delete role group failed on save");
            }

            return Ok(id);
        }
    }
}