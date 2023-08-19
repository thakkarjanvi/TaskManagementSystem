using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystemProject.Model;

namespace TaskManagementSystemProject.Context
{
    public class ApplicationDbContext : IdentityDbContext
           
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<TaskComment> TaskComments { get; set; }
    }
}
