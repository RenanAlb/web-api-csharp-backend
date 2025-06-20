using Microsoft.EntityFrameworkCore;
using WebAPI_Person.Models;

namespace WebAPI_Person.Data
{
    public class PersonContext : DbContext
    {
        public DbSet<PersonModel> People { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=person.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
