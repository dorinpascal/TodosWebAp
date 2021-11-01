using Microsoft.EntityFrameworkCore;
using TodosWebAp.Model;

namespace TodosWebAp.Persistence
{
    public class TodoContext:DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // name of database
            optionsBuilder.UseSqlite("Data Source = Todos.db");
        }

    }
}