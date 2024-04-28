using CSAcademyToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace CSAcademyToDoList.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
