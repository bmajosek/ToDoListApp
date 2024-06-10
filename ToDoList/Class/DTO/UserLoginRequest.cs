using System.ComponentModel.DataAnnotations;

namespace ToDoList.Class.DTO
{
    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}