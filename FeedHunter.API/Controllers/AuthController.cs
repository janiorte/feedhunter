using AutoMapper;
using FeedHunter.API.Model;
using FeedHunter.API.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FeedHunter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtTokenService jwtTokenService;

        public AuthController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager,
            IJwtTokenService jwtTokenService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            var user = mapper.Map<User>(userForRegister);

            var result = await userManager.CreateAsync(user, userForRegister.Password);

            if (result.Succeeded)
            {
                return StatusCode(201);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            var user = await userManager.FindByNameAsync(userForLogin.Username);

            if (user == null) return Unauthorized();

            var result = await signInManager.CheckPasswordSignInAsync(user, userForLogin.Password, false);

            if (result.Succeeded)
            {
                return Ok(new { token = jwtTokenService.GenerateJwtToken(user) });
            }

            return Unauthorized();
        }
    }
}