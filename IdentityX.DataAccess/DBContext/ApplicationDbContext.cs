using EntityFramework.Exceptions.SqlServer;
using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentityX.DataAccess.DBContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, Guid>
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    public DbSet<ApplicationEntity> Applications { get; set; }
    public DbSet<ApplicationUserRelationsEntity> ApplicationUserRelations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        if (_configuration["ASPNETCORE_ENVIRONMENT"] != "Development")
        {
            modelBuilder.Entity<ApplicationUserRelationsEntity>().ToContainer("ApplicationUserRelations");
            modelBuilder.Entity<ApplicationEntity>().ToContainer("Applications");
            modelBuilder.Entity<ApplicationUser>().ToContainer("ApplicationUser");
            modelBuilder.Entity<ApplicationRole>().ToContainer("ApplicationRole");
        }
        modelBuilder.Entity<ApplicationEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ApplicationEntity>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ApplicationEntity>().HasIndex(x => x.NormalizedName).IsUnique();
        modelBuilder.Entity<ApplicationEntity>().Property(x => x.Active).HasDefaultValue(true);
        
        // ApplicationUserRelationsEntity 
        //
        modelBuilder.Entity<ApplicationUserRelationsEntity>().HasKey(x => new { x.Id,x.ApplicationUserId, x.ApplicationRoleId });
        modelBuilder.Entity<ApplicationUserRelationsEntity>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<ApplicationUserRelationsEntity>(
            entity => entity.HasOne(aur => aur.Application).WithMany(a => a.ApplicationUserRelations)
                .HasForeignKey(aur => aur.ApplicationId)
        );
        modelBuilder.Entity<ApplicationUserRelationsEntity>(entity => entity.HasOne(aur => aur.ApplicationUser).WithMany(a => a.ApplicationUserRelations).HasForeignKey(aur => aur.ApplicationUserId));
        modelBuilder.Entity<ApplicationUserRelationsEntity>().HasOne(aur => aur.ApplicationRole).WithMany(u => u.ApplicationUserRelations).HasForeignKey(x => x.ApplicationRoleId);
        
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }
    private void SetTimestamps()
    {
        var entries = ChangeTracker.Entries<ApplicationEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
                entry.Entity.UpdatedOn = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedOn = DateTime.UtcNow;
            }
        }
    }
    
    

}