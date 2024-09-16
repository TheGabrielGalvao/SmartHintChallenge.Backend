using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region DbSet
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<SettingsEntity> Settings { get; set; }
        #endregion


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            GenerateGuidForNewEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void GenerateGuidForNewEntities()
        {
            var entitiesWithDefaultEntity = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity.GetType().GetProperty("Uuid") != null)
                .Select(e => e.Entity)
                .ToList();

            foreach (var entity in entitiesWithDefaultEntity)
            {
                var uuidProperty = entity.GetType().GetProperty("Uuid");
                if (uuidProperty != null && uuidProperty.PropertyType == typeof(Guid))
                {
                    var currentUuid = (Guid)uuidProperty.GetValue(entity);
                    if (currentUuid == Guid.Empty)
                    {
                        uuidProperty.SetValue(entity, Guid.NewGuid());
                    }
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingsEntity>().HasData(
                new SettingsEntity
                {
                    Id = 1, // Valor fixo fornecido explicitamente
                    StateRegistrationForIndividual = false
                }
             );

            base.OnModelCreating(modelBuilder);
           
        }
    }
}
