using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.API.Dto;
using Users.Businness.Abstract;
using Users.Entities.Entities.Concreate;

namespace Users.API.Controllers
{
    [Route("user")]
    //[Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, IDepartmentService departmentService)
        {
            _userService = userService;
            _mapper = mapper;
            _departmentService = departmentService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<ActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            if (users != null)
            {
                List<UserDto> usersDtos = new List<UserDto>();
                foreach (var user in users)
                {
                    UserDto userDto = _mapper.Map<UserDto>(user);
                    userDto.Department =_departmentService.GetByIdAsync(user.DepartmentId).Result;
                    usersDtos.Add(userDto);
                }
                return Ok(usersDtos);
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet]
        [Route("GetByIdAsync/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user != null)
            {
                return Ok(_mapper.Map<UserDto>(user));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddAsync")]
        public async Task<ActionResult> AddAsync([FromBody] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<User>(userDto);
                user.CreatedOn = DateTime.Now;
                await _userService.AddAsync(user);
                return Ok(userDto);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateAsync")]
        public async Task<ActionResult> UpdateAsync([FromBody] UserDto userDto)
        {
            var oldUser = await _userService.GetByIdAsync(userDto.UserId);
            if (oldUser != null)
            {
                User user = _mapper.Map<User>(userDto);
                user.CreatedOn = oldUser.CreatedOn;
                user.UpdatedOn = DateTime.Now;
                await _userService.UpdateAsync(user);
                return Ok(userDto);
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet]
        [Route("DeleteAsync/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user != null)
            {
                await _userService.DeleteAsync(id);
                return Ok(_mapper.Map<UserDto>(user)); ;
            }
            return NotFound();
        }
    }
}
