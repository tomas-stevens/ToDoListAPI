using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;


namespace ToDoListAPI.Data
{
    public class ToDoDataBaseContext : DbContext
    {
        public ToDoDataBaseContext(DbContextOptions<ToDoDataBaseContext> options) : base(options)
        {
        }
        public DbSet<ToDoList> ToDoLists { get; set; }
    }
}
