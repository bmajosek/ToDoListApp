using Microsoft.AspNetCore.Mvc;
using ToDoList.Interface;
using ToDoList.Class.DTO;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResult
                {
                    Errors = ["Invalid payload"],
                    Result = false
                });

            var result = await _authenticationService.Register(requestDto);
            if (!result.Result)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResult
                {
                    Errors = ["Invalid payload"],
                    Result = false
                });

            var loginResult = await _authenticationService.Login(loginRequest);
            if (!loginResult.Result)
                return BadRequest(loginResult);

            return Ok(loginResult);
        }
    }
}