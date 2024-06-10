namespace ToDoList.Model
{
    public class TaskToDo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
        public ApiUser User { get; set; }
        public string FamilyId { get; set; }
    }
}