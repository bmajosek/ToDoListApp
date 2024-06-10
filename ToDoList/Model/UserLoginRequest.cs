using System.ComponentModel.DataAnnotations;

namespace ToDoList.Model
{
    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}