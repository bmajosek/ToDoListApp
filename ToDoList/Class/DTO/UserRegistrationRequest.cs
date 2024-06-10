using System.ComponentModel.DataAnnotations;

namespace ToDoList.Class.DTO
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [MinLength(6)]
        public string FamilyId { get; set; }
    }
}