using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CORE.API.Controllers.Dto;
using CORE.API.Core.IRepository;
using CORE.API.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CORE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleRightController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IModuleRightRepository moduleRepository;
        private readonly IUnitOfWork unitOfWork;

        public ModuleRightController(IMapper mapper, IModuleRightRepository moduleRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.moduleRepository = moduleRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var modules = await moduleRepository.GetAll();
            var result = mapper.Map<IEnumerable<ViewModuleDto>>(modules);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var module = await moduleRepository.GetById(id);

            if (module == null)
            {
                return NotFound();
            }

            var result = mapper.Map<ModuleRight, ViewModuleDto>(module);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveModuleDto moduleDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var module = mapper.Map<SaveModuleDto, ModuleRight>(moduleDto);
            moduleRepository.Add(module);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Create new module failed on save");
            }

            module = await moduleRepository.GetById(module.Id);
            var result = mapper.Map<ModuleRight, ViewModuleDto>(module);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveModuleDto moduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var module = await moduleRepository.GetById(id);
            if (module == null)
            {
                return NotFound();
            }

            module = mapper.Map(moduleDto, module);
            moduleRepository.Update(module);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Update module failed on save");
            }

            module = await moduleRepository.GetById(module.Id);
            var result = mapper.Map<ModuleRight, ViewModuleDto>(module);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var module = await moduleRepository.GetById(id);
            if (module == null)
            {
                return NotFound();
            }

            moduleRepository.Remove(module);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Delete module failed on save");
            }

            return Ok(id);
        }

    }
}