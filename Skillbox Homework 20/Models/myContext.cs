using Microsoft.EntityFrameworkCore;

namespace Skillbox_Homework_20.Database
{
    public class myContext : DbContext
    {
        public DbSet<Information> Information { get; set; }

        public myContext(DbContextOptions<myContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public myContext()
        {

        }
    }
}
