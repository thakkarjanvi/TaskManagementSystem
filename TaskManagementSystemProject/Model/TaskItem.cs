using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystemProject
{
    public class TaskItem
    {
        public int Id { get; set; } // Unique identifier for the task
        public string Title { get; set; } // Title of the task
        public string Description { get; set; } // Description of the task
        public TaskStatus Status { get; set; } // Status of the task (Todo, In-Progress, Done)
        public DateTime DueDate { get; set; } // Due date of the task
        public TaskPriority Priority { get; set; } // Priority of the task (High, Medium, Low)
        public string? AssignId { get; set; } // Unique identifier of the assigned user

        public IdentityUser? IdentityUser { get; set; }

    }
    public enum TaskStatus
    {
        Todo = 1,
        InProgress = 2,
        Done = 3
    }

    public enum TaskPriority
    {
        High = 1,
        Medium = 2,
        Low =3
    }
    public static class TaskDataStore
    {
        public static List<TaskItem> Tasks { get; } = new List<TaskItem>();
    }
}
