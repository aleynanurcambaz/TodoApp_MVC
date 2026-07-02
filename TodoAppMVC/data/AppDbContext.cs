using Microsoft.EntityFrameworkCore;
using TodoAppMVC.Models; //Todo modelin sistemi tanıması için
namespace TodoAppMVC.Data
{     public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; } //Todo modelini temsil eden DbSet
    }
}

