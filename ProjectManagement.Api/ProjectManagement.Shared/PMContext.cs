using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;


namespace ProjectManagement.Shared
{
    public class PMContext<T> : DbContext where T : BaseEntity
    {
        public PMContext(DbContextOptions<PMContext<T>> context) : base(context)
        {
        }
        public DbSet<T> Table { get; set; }

    }
}
