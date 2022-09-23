using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Dto;
using Users.Businness.Abstract;

namespace Users.API.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public LoginController(IUserService userService, IMapper mapper, ITokenService tokenService)
        {
            _mapper = mapper;
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("LoginAsync")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto.UserName, loginDto.Password);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Token = _tokenService.CreateToken(loginDto.UserName, loginDto.Password);
                return Ok(userDto); ;
            }
            return NotFound();
        }
    }
}
