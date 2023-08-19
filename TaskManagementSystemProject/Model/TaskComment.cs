namespace TaskManagementSystemProject.Model
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; } // Foreign key to the user who made the comment
        public int TaskId { get; set; }    // Foreign key to the task being commented on

    }
}
