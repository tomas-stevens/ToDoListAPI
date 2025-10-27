namespace ToDoListAPI.Models
{
    public class ToDoList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int Priority { get; set; }

        public ToDoList()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
