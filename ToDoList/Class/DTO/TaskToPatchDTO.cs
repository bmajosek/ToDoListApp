namespace ToDoList.Class.DTO
{
    public class TaskToPatchDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}