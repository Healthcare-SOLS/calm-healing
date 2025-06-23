//using Calm_Healing.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Calm_Healing.DAL.DynamicModels
{
    public class DynamicTenantDbContext : DbContext
    {
        private readonly string _schema;

        public DynamicTenantDbContext(DbContextOptions<DynamicTenantDbContext> options, string schema)
            : base(options)
        {
            _schema = schema;
        }

        public DbSet<GroupUser> Users { get; set; }
        public DbSet<GroupPatient> Patients { get; set; }
        public DbSet<GroupProvider> Providers { get; set; }
        public DbSet<GroupAddress> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);

            // You can optionally uncomment and configure table mappings
            base.OnModelCreating(modelBuilder);
        }
    }
}
