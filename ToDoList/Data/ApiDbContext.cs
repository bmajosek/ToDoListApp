using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Model;

namespace ToDoList.Data
{
    public class ApiDbContext : IdentityDbContext<ApiUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApiUser> ApiUsers { get; set; }
        public DbSet<TaskToDo> TasksToDo { get; set; }
    }
}