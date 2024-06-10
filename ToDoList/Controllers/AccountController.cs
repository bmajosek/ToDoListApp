using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interface;
using ToDoList.Services.Interfaces;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _usersRepository;

        public AccountController(IUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet("GetFamilyMembers"), Authorize]
        public async Task<IActionResult> GetFamilyMembers()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
                return Unauthorized("User has to be logged");
            var emails = await _usersRepository.GetFamilyMembersEmails(userId);

            return Ok(emails);
        }
    }
}