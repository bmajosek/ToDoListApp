using Microsoft.AspNetCore.Identity;

namespace ToDoList.Model
{
    public class ApiUser : IdentityUser
    {
        public ICollection<TaskToDo> Tasks { get; set; }
        public string FamilyId { get; set; }
    }
}