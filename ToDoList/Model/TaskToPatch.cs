namespace ToDoList.Model
{
    public class TaskToPatch
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}