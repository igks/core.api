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
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private IUnitOfWork unitOfWork;

        public UserController(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userRepository.GetAll();
            var result = mapper.Map<IEnumerable<ViewUserDto>>(users);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var user = await userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = mapper.Map<User, ViewUserDto>(user);
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] UserParams userParams)
        {
            var users = await userRepository.GetPaged(userParams);
            var result = mapper.Map<IEnumerable<ViewUserDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = mapper.Map<AddUserDto, User>(userDto);
            var password = userDto.Password;
            userRepository.Add(user, password);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: "Create user data failed on save");
            }

            user = await userRepository.GetById(user.Id);
            var result = mapper.Map<User, ViewUserDto>(user);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EditUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            user = mapper.Map(userDto, user);
            userRepository.Update(user);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: "Update user failed in save");
            }

            user = await userRepository.GetById(user.Id);
            var result = mapper.Map<User, ViewUserDto>(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            userRepository.Remove(user);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: "Delete department failed on save");
            }

            return Ok(id);
        }
    }
}