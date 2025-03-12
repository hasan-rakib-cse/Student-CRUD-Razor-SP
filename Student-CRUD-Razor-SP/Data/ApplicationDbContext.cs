using Microsoft.EntityFrameworkCore;
using Student_CRUD_Razor_SP.Models;

namespace Student_CRUD_Razor_SP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;
    }
}
